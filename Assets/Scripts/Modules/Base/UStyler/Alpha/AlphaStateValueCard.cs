using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(AlphaStateValueCard), menuName = "UStyler/"+nameof(AlphaStateValueCard), order = 0)]
    public class AlphaStateValueCard : AlphaBaseStateCard
    { 
        [System.Serializable]
        public class AlphaValueState : StyleState<float>
        {
            [SerializeField, Range(0, 1)] private float value;
            public override float Value => value;
        }

        [SerializeField] private AlphaValueState[] states;

        public override float Get(StateCard state)
        {
            for (int i = 0; i < states.Length; i++)
                if (states[i].State == state)
                    return states[i].Value;

            return default;
        }
    }
}