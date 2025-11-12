using System.Collections.Generic;
using UnityEngine;

namespace PP3.Core
{
    [CreateAssetMenu(menuName = "PP3/Configs/VFX Config")]
    public class VFXConfigs : ScriptableObject
    {
        [System.Serializable]
        public struct VFXEntry
        {
            public string id;          // e.g. "muzzle", "explosion", "smoke"
            public GameObject prefab;  // prefab reference
            public float lifetime;     // destroy time after spawn
        }

        public List<VFXEntry> effects = new();

        public bool TryGet(string id, out VFXEntry entry)
        {
            foreach (var e in effects)
                if (e.id == id) { entry = e; return true; }

            entry = default;
            return false;
        }
    }
}
