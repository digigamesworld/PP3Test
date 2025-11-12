using UnityEngine;

namespace UStylers
{
    public abstract class StyleCard : ScriptableObject
    {

    }

    public abstract class StyleCard<T> : StyleCard
    {
        public virtual T Value { get; }
    }
}