using UnityEngine;

namespace UStylers
{
    public abstract class ComponentStyler<T> : MonoBehaviour
    where T: Component
    {
        private T comp;
        public T Comp => comp ??= GetComponent<T>();
        private UStylerBase[] stylers;
        protected UStylerBase[] Stylers => 
            stylers ??= GetComponentsInChildren<UStylerBase>();
        public virtual bool Interactable { set; get;}

        public virtual void SetStyle(StateCard card)
        {
            for(int i=0;i< Stylers.Length;i++)
                Stylers[i].SetState(card);
        }
               
        public virtual void Selected() =>
            SetStyle(DefaultStates.Instance.Selected);
            
        public virtual void Default() =>
            SetStyle(DefaultStates.Instance.Default);
                        
        public virtual void Deactive() =>
            SetStyle(DefaultStates.Instance.Deactive);

        public virtual void Hover() =>
            SetStyle(DefaultStates.Instance.Hover);
    }
}