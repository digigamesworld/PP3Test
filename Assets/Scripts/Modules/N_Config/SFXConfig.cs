using UnityEngine;
using System.Collections.Generic;

namespace PP3.Core
{
    [CreateAssetMenu(menuName = "PP3/Configs/SFX Config")]
    public class SFXConfig : ScriptableObject
    {
        [System.Serializable]
        public struct Entry
        {
            public string id;
            public AudioClip clip;
        }

        public List<Entry> entries = new();
    }
}


