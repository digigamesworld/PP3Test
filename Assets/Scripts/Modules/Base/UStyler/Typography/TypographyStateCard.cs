using UnityEngine;

namespace UStylers
{
    public abstract class TypographyBaseStateCard : StyleBaseStateCard<TypographyStyle> { }

    [CreateAssetMenu(fileName = nameof(TypographyStateCard), menuName = "UStyler/"+nameof(TypographyStateCard), order = 0)]
    public class TypographyStateCard : TypographyBaseStateCard 
    {
        [System.Serializable]
        public class TypographyState : StyleState<TypographyCard,TypographyStyle> 
        {
            public override TypographyStyle Value => Card.Value;
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