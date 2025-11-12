    using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(TypographyTMProStateValueCard), menuName = "UStyler/" + nameof(TypographyTMProStateValueCard), order = 0)]
    public class TypographyTMProStateValueCard : TypographyTMProBaseStateCard
    {
        [System.Serializable]
        public class TypographyTMProState : StyleState<TypographyTMProStyle>
        {
            [SerializeField] private TypographyTMProStyle style;
            public override TypographyTMProStyle Value => style;
        }

        [SerializeField] private TypographyTMProState[] states;

        public override TypographyTMProStyle Get(StateCard state)
        {
            for (int i = 0; i < states.Length; i++)
                if (states[i].State == state)
                    return states[i].Value;

            return default;
        }
    }
}