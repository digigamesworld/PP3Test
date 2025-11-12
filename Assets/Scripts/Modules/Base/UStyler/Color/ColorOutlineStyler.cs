using UnityEngine;
using UnityEngine.UI;

namespace UStylers
{
    public class ColorOutlineStyler : UStyler<Color,ColorBaseStateCard,ColorCard, Outline>
    {
        public override void SetStyle(Color state)
        {         
            if (styleComp ??= GetComponent<Outline>())
               styleComp.effectColor = state;
        }

        #if UNITY_EDITOR
        [ContextMenu(nameof(ApplyState))]
        public override void ApplyState() =>
            base.ApplyState();
        #endif
    }
}