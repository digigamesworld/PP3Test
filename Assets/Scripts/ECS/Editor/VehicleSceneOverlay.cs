#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;

[InitializeOnLoad]
public static class VehicleSceneOverlay
{
    // UI State
    static string _search = "";
    static int _maxList = 50;
    static float _gizmoSize = 0.35f;
    static bool _showLabels = true;
    static bool _followSelected = false;

    static Entity _selected = Entity.Null;

    // Optional: reflection helper to try selecting the entity in Entities Hierarchy (if your editor exposes it)
    static Action<Entity> _selectEntityInEntitiesWindow;

    static VehicleSceneOverlay()
    {
        SceneView.duringSceneGui += OnSceneGUI;

        // Try to bind a public selection API if available (varies by Entities Editor version).
        // If your Unity exposes Unity.Entities.Editor.EntitySelectionProxy.SelectEntity(Entity),
        // we hook it up here via reflection to avoid hard deps / compile breaks.
        TryBindSelectionInEntitiesWindow();
    }

    static void OnSceneGUI(SceneView sv)
    {
        var list = VehicleGizmoCollectorSystem.Vehicles;
        if (!list.IsCreated) return;

        // —— 3D world gizmos ——
        Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

        for (int i = 0; i < list.Length; i++)
        {
            var v = list[i];
            if (!PassesSearch(v.Name)) continue;

            bool isSelected = v.Entity == _selected;

            Handles.color = isSelected ? Color.yellow : Color.green;
            Handles.SphereHandleCap(0, v.Position, Quaternion.identity, _gizmoSize, EventType.Repaint);

            if (_showLabels)
            {
                Handles.Label(v.Position + new float3(0, 1.0f, 0), v.Name.ToString());
            }
        }

        // If follow mode is on, keep the SceneView camera pivot on selected
        if (_followSelected && _selected != Entity.Null)
        {
            if (TryGetSelectedPosition(out float3 pos))
            {
                sv.pivot = pos;
                sv.Repaint();
            }
        }

        // —— 2D IMGUI overlay ——
        Handles.BeginGUI();
        {
            const int width = 280;
            GUILayout.BeginArea(new Rect(10, 10, width, 400), "Vehicle Debugger", GUI.skin.window);

            GUILayout.Label("Search");
            var newSearch = GUILayout.TextField(_search);
            if (newSearch != _search) _search = newSearch;

            GUILayout.Space(4);
            _showLabels = GUILayout.Toggle(_showLabels, "Show Labels");
            _followSelected = GUILayout.Toggle(_followSelected, "Follow Selected");

            GUILayout.Space(4);
            GUILayout.Label($"Gizmo Size: {_gizmoSize:0.00}");
            _gizmoSize = GUILayout.HorizontalSlider(_gizmoSize, 0.05f, 1.2f);

            GUILayout.Space(4);
            GUILayout.Label($"Max List: {_maxList}");
            _maxList = Mathf.Clamp(
                (int)GUILayout.HorizontalSlider(_maxList, 10, 500),
                10, 1000);

            GUILayout.Space(6);
            GUILayout.Label($"Active (post-filter): {CountVisible(list)}");

            GUILayout.Space(4);
            DrawList(sv, list, width);

            GUILayout.EndArea();
        }
        Handles.EndGUI();

        SceneView.RepaintAll();
    }

    static void DrawList(SceneView sv, Unity.Collections.NativeList<VehicleGizmoCollectorSystem.VehicleData> list, int width)
    {
        // Scroll view for the vehicle buttons
        using (var scroll = new GUILayout.ScrollViewScope(Vector2.zero, GUILayout.Width(width - 14), GUILayout.Height(220)))
        {
            int shown = 0;
            for (int i = 0; i < list.Length; i++)
            {
                var v = list[i];
                if (!PassesSearch(v.Name)) continue;

                var rowStyle = (v.Entity == _selected) ? EditorStyles.miniButtonMid : EditorStyles.miniButton;
                if (GUILayout.Button(v.Name.ToString(), rowStyle))
                {
                    _selected = v.Entity;

                    // Focus SceneView camera on that entity
                    sv.pivot = v.Position;
                    sv.Repaint();

                    // Try to also select in Entities Hierarchy (if API bound)
                    _selectEntityInEntitiesWindow?.Invoke(v.Entity);
                }

                shown++;
                if (shown >= _maxList)
                {
                    GUILayout.Label("…");
                    break;
                }
            }
        }

        GUILayout.Space(4);
        using (new GUILayout.HorizontalScope())
        {
            using (new EditorGUI.DisabledScope(_selected == Entity.Null))
            {
                if (GUILayout.Button("Focus Selected"))
                {
                    if (TryGetSelectedPosition(out float3 p))
                    {
                        sv.pivot = p;
                        sv.Repaint();
                    }
                }

                if (GUILayout.Button(_followSelected ? "Stop Follow" : "Follow"))
                {
                    _followSelected = !_followSelected;
                }
            }

            if (GUILayout.Button("Clear Selection"))
            {
                _selected = Entity.Null;
                _followSelected = false;
            }
        }
    }

    static bool PassesSearch(FixedString64Bytes name)
    {
        if (string.IsNullOrEmpty(_search)) return true;
        return name.ToString().IndexOf(_search, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    static int CountVisible(Unity.Collections.NativeList<VehicleGizmoCollectorSystem.VehicleData> list)
    {
        if (string.IsNullOrEmpty(_search)) return list.Length;
        int c = 0;
        for (int i = 0; i < list.Length; i++)
            if (PassesSearch(list[i].Name)) c++;
        return c;
    }

    static bool TryGetSelectedPosition(out float3 pos)
    {
        pos = default;
        var list = VehicleGizmoCollectorSystem.Vehicles;
        if (!list.IsCreated || _selected == Entity.Null) return false;

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].Entity == _selected)
            {
                pos = list[i].Position;
                return true;
            }
        }
        return false;
    }

    static void TryBindSelectionInEntitiesWindow()
    {
        // Best-effort: reflect a public editor API IF your Entities editor package exposes it publicly.
        // If not found, we simply skip “select in Entities Hierarchy”.
        try
        {
            // Example target (varies by package version):
            // Unity.Entities.Editor.EntitySelectionProxy.SelectEntity(Entity e)
            var editorAsm = AppDomain.CurrentDomain.Load("Unity.Entities.Editor");
            if (editorAsm == null) return;

            var proxyType = editorAsm.GetType("Unity.Entities.Editor.EntitySelectionProxy");
            if (proxyType == null) return;

            var method = proxyType.GetMethod("SelectEntity", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(Entity) }, null);
            if (method == null) return;

            _selectEntityInEntitiesWindow = (Action<Entity>)Delegate.CreateDelegate(typeof(Action<Entity>), method);
        }
        catch
        {
            // Silently ignore if editor API is not available.
            _selectEntityInEntitiesWindow = null;
        }
    }
}
#endif
