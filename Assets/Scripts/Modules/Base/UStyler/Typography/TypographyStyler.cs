using UnityEngine;
using UnityEngine.UI;

namespace UStylers
{
    public class TypographyStyler : UStyler<TypographyStyle,TypographyStateCard,TypographyCard, Text>
    {
        public override void SetStyle(TypographyStyle style)
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