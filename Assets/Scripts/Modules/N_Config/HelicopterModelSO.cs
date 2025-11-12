using UnityEngine;

namespace PP3.Core
{
    [CreateAssetMenu(menuName = "PP3/Configs/HelicoopterModel")]
    public class HelicopterModelSO : ScriptableObject
    {
        public float yawRate = 1.5f; public float pitchRate = 1.2f; public float rollRate = 1.2f; public float thrust = 12f;
    }
}