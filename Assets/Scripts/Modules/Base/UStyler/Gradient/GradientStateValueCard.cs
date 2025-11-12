using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(GradientStateValueCard), menuName = "UStyler/"+nameof(GradientStateValueCard), order = 0)]
    public class GradientStateValueCard : GradientBaseStateCard
    { 
        [System.Serializable]
        public class GradientValueState : StyleState<GradientStyle>
        {
            [SerializeField] private GradientStyle style;
            public override GradientStyle Value => style;
        }

        [SerializeField] private GradientValueState[] states;

        public override GradientStyle Get(StateCard state)
        {
            for (int i = 0; i < states.Length; i++)
                if (states[i].State == state)
                    return states[i].Value;

            return default;
        }
    }
}