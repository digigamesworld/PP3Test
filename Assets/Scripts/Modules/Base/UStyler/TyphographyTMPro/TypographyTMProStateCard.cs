using UnityEngine;

namespace UStylers
{
    public abstract class TypographyTMProBaseStateCard : StyleBaseStateCard<TypographyTMProStyle> { }

    [CreateAssetMenu(fileName = nameof(TypographyTMProStateCard), menuName = "UStyler/" + nameof(TypographyTMProStateCard), order = 0)]
    public class TypographyTMProStateCard : TypographyTMProBaseStateCard
    {
        [System.Serializable]
        public class TypographyTMProState : StyleState<TypographyTMProCard, TypographyTMProStyle>
        {
            public override TypographyTMProStyle Value => Card.Value;
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