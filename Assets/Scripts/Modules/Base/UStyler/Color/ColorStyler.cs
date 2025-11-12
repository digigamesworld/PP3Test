using UnityEngine;
using UnityEngine.UI;

namespace UStylers
{
    public class ColorStyler : UStyler<Color,ColorBaseStateCard,ColorCard, Graphic>
    {
        public override void SetStyle(Color state)
        {         
            if (styleComp ??= GetComponent<Graphic>())
               styleComp.color = state;
        }

        #if UNITY_EDITOR
        [ContextMenu(nameof(ApplyState))]
        public override void ApplyState() =>
            base.ApplyState();
        #endif
    }
}