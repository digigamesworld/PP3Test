using UnityEngine;

namespace UStylers
{
    public abstract class ColorBaseStateCard : StyleBaseStateCard<Color> { }

    [CreateAssetMenu(fileName = nameof(ColorStateCard), menuName = "UStyler/"+nameof(ColorStateCard), order = 0)]
    public class ColorStateCard : ColorBaseStateCard 
    {
        [System.Serializable]
        public class ColorState : StyleState<ColorCard,Color> 
        {
            public override Color Value => Card.Value;
        }

        [SerializeField] private ColorState[] states;

        public override Color Get(StateCard state)
        {
            for (int i = 0; i < states.Length; i++)
                if (states[i].State == state)
                    return states[i].Value;

            return default;
        }
    }
}