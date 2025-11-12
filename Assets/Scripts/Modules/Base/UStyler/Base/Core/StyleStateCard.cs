using UnityEngine;

namespace UStylers
{
    public abstract class StyleBaseStateCard<TValue> : ScriptableObject
    {
        public virtual TValue Get(StateCard state) => default;
    }
    
    [System.Serializable]
    public abstract class StyleState<TCard,TValue> : StyleState<TValue> 
    where TCard : StyleCard
    {
        [field:SerializeField] public TCard Card {private set; get;} 
    }

    [System.Serializable]
    public abstract class StyleState<TValue>
    {
        [field:SerializeField] public StateCard State {private set; get;}

        public virtual TValue Value {get;}
    }
}