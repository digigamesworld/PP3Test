using UnityEngine;

namespace UStylers
{
    public abstract class GradientBaseStateCard : StyleBaseStateCard<GradientStyle> { }

    [CreateAssetMenu(fileName = nameof(GradientStateCard), menuName = "UStyler/"+nameof(GradientStateCard), order = 0)]
    public class GradientStateCard : GradientBaseStateCard 
    {
        [System.Serializable]
        public class GradientState : StyleState<GradientCard,GradientStyle> 
        {
            public override GradientStyle Value => Card.Value;
        }

        [SerializeField] private GradientState[] states;

        public override GradientStyle Get(StateCard state)
        {
            for (int i = 0; i < states.Length; i++)
                if (states[i].State == state)
                    return states[i].Value;

            return default;
        }
    }
}