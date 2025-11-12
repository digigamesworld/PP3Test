using UnityEngine;

namespace UStylers
{
    public abstract class AlphaBaseStateCard : StyleBaseStateCard<float> { }

    [CreateAssetMenu(fileName = nameof(AlphaStateCard), menuName = "UStyler/"+nameof(AlphaStateCard), order = 0)]
    public class AlphaStateCard : AlphaBaseStateCard
    { 
        [System.Serializable]
        public class AlphaState : StyleState<AlphaCard, float>
        {
            public override float Value => Card.Value;
        }

        [SerializeField] private AlphaState[] states;

        public override float Get(StateCard state)
        {
            for (int i = 0; i < states.Length; i++)
                if (states[i].State == state)
                    return states[i].Value;

            return default;
        }
    }
}