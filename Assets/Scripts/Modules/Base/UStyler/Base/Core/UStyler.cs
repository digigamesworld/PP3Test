using UnityEngine;

namespace UStylers
{
    public abstract class UStyler<TValue,TStyleStateCard,TStyleCard, TStyleComp> : UStylerBase
    where TStyleCard  : StyleCard<TValue>
    where TStyleComp  : Component
    where TStyleStateCard : StyleBaseStateCard<TValue>
    {
        [SerializeField] protected TStyleCard defaultState;
        [SerializeField] protected TStyleStateCard stateCard;
        protected TStyleComp styleComp;
        protected override void Awake()
        {
            base.Awake();
            styleComp ??= GetComponent<TStyleComp>();
        }

        public virtual void ApplyState() 
        {
            if (defaultState)
                SetStyle(defaultState);
            else if(stateCard != null && DefaultStates.Instance?.Default) 
                SetState(DefaultStates.Instance.Default);
        }

        public override void ApplyState(StateCard state)
        {
            if(stateCard == null) return;
            base.ApplyState(state);
            SetStyle(stateCard.Get(state));
        }

        public virtual void SetStyle(TStyleCard style)
        {         
            if (style)
              SetStyle(style.Value);
        }
        public abstract void SetStyle(TValue value);

        #if UNITY_EDITOR
        private void OnValidate() 
        {
            styleComp ??= GetComponent<TStyleComp>();
            ApplyState();
        }
        #endif
    }
}