using TMPro;
using UnityEngine;

namespace UStylers
{
    public class TypographyTMProStyler : UStyler<TypographyTMProStyle,TypographyTMProStateCard, TypographyTMProCard,TextMeshProUGUI >
    {
        public override void SetStyle(TypographyTMProStyle style)
        {
            if (!styleComp)
                return;

            styleComp.font = style.Asset;
            styleComp.fontStyle = style.Style;
            styleComp.fontSize = style.Size;
        }

        #if UNITY_EDITOR
        [ContextMenu("Apply Style")]
        public override void ApplyState() =>
            base.ApplyState();
        #endif
    }
}