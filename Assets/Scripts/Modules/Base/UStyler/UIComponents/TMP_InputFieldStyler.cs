using UnityEngine;
using TMPro;

namespace UStylers.UIComponents
{
    [RequireComponent(typeof(TMP_InputField))]
    public class TMP_InputFieldStyler : ComponentStyler<TMP_InputField>
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
    }
}