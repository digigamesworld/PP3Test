using System.Collections.Generic;
using PP3.Modules.Vehicle;
using UnityEngine;

public class VehicleAttachmentManager : MonoBehaviour
{
    [SerializeField] private List<VehicleAttachment> defaultAttachments = new();
    private readonly Dictionary<PartSlotId, GameObject> _mounted = new();

    public void InitializeDefaults()
    {
        foreach (var att in defaultAttachments)
            if (att.enabled) Attach(att.slot, att.prefab);
    }

    public void Attach(PartSlotId slot, GameObject prefab)
    {
        var mount = FindSlot(slot);
        if (mount == null || prefab == null) return;

        // Remove existing
        if (_mounted.TryGetValue(slot, out var old))
            Destroy(old);

        // Instantiate new
        var go = Instantiate(prefab, mount.GetMountPoint());
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        _mounted[slot] = go;
    }

    public void Detach(PartSlotId slot)
    {
        if (_mounted.TryGetValue(slot, out var go))
        {
            Destroy(go);
            _mounted.Remove(slot);
        }
    }

    private PartSlot FindSlot(PartSlotId id)
    {
        foreach (var s in GetComponentsInChildren<PartSlot>())
            if (s.id == id) return s;
        return null;
    }
}
