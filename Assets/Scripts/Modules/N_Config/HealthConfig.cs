using UnityEngine;

namespace PP3.Core
{
    [CreateAssetMenu(menuName = "PP3/Configs/Health Config")]
    public class HealthConfig : ScriptableObject
    {
        [Range(10, 1000)] public float Max = 100f;
    }
}