using UnityEngine;
using UnityEngine.UI;

namespace UStylers
{
    public class GradientStyler : UStyler<GradientStyle,GradientBaseStateCard,GradientCard, UIGradients.UIGradient>
    {
        private Graphic graphic;

        public override void SetStyle(GradientStyle state)
        {
            if (!styleComp)
                return;

            styleComp.m_color1 = state.Color1;
            styleComp.m_color2 = state.Color2;
            (graphic ??= styleComp.GetComponent<Graphic>()).SetAllDirty();
        }

        #if UNITY_EDITOR
        [ContextMenu("Apply Style")]
        public override void ApplyState() =>
            base.ApplyState();
        #endif
    }
}