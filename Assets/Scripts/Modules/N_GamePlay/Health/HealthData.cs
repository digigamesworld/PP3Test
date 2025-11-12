using UnityEngine;

namespace PP3.Core
{
    [System.Serializable]
    public struct HealthData
    {
        public float Max;
        public float Current;

        public HealthData(float max)
        {
            Max = max;
            Current = max;
        }
    }
}
