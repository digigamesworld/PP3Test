using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UStylers.UIComponents
{
    [RequireComponent(typeof(Button))]
    public class ButtonStyler : ComponentStyler<Button>
    {
        public override bool Interactable
        {
            set 
            {
                if(value == Comp.interactable)
                    return;

                Comp.interactable = value;
                SetStyle(value? DefaultStates.Instance.Default :DefaultStates.Instance.Deactive);
            }

            get => Comp.interactable;
        }

        public void SetAction(UnityAction onClick)
        {
            Comp.onClick.RemoveAllListeners();
            Comp.onClick.AddListener(onClick);
        } 
    }
}