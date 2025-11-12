    using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(TypographyStateValueCard), menuName = "UStyler/" + nameof(TypographyStateValueCard), order = 0)]
    public class TypographyStateValueCard : TypographyBaseStateCard
    {
        [System.Serializable]
        public class TypographyState : StyleState<TypographyStyle>
        {
            [SerializeField] private TypographyStyle style;
            public override TypographyStyle Value => style;
        }

        [SerializeField] private TypographyState[] states;

        public override TypographyStyle Get(StateCard state)
        {
            for (int i = 0; i < states.Length; i++)
                if (states[i].State == state)
                    return states[i].Value;

            return default;
        }
    }
}