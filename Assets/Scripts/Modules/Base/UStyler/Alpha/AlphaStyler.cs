using UnityEngine;
using UnityEngine.UI;

namespace UStylers
{
    public class AlphaStyler : UStyler<float,AlphaBaseStateCard,AlphaCard, Graphic>
    {  
        public override void ApplyState(StateCard state)
        {
            if(!stateCard) return;
            base.ApplyState(state);

            SetStyle(stateCard.Get(state));
        }

        public override void SetStyle(AlphaCard style)
        {         
            if (style)
             SetStyle(style.Value);
        }

        public override void SetStyle(float value)
        {         
            if (styleComp)
                styleComp.color = new Color(styleComp.color.r, styleComp.color.g, styleComp.color.b, value);
        }

        #if UNITY_EDITOR
        [ContextMenu("Apply Style")]
        public override void ApplyState() =>
            base.ApplyState();
        #endif
    }
}