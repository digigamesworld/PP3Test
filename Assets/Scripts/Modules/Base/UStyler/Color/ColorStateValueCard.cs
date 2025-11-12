using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(ColorStateValueCard), menuName = "UStyler/"+nameof(ColorStateValueCard), order = 0)]
    public class ColorStateValueCard : ColorBaseStateCard
    { 
        [System.Serializable]
        public class ColorValueState : StyleState<Color>
        {
            [SerializeField] private Color value;
            public override Color Value => value;
        }

        [SerializeField] private ColorValueState[] states;

        public override Color Get(StateCard state)
        {
            for (int i = 0; i < states.Length; i++)
                if (states[i].State == state)
                    return states[i].Value;

            return default;
        }
    }
}