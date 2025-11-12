using TMPro;
using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(TypographyTMProCard), menuName = "UStyler/"+nameof(TypographyTMProCard), order = 0)]
    public class TypographyTMProCard : StyleCard<TypographyTMProStyle>
    {
        [SerializeField] private float size = 16;
        [SerializeField] private TMP_FontAsset asset;
        [SerializeField] private FontStyles style;
        public override TypographyTMProStyle Value => new(size, asset, style);
    }

    [System.Serializable]
    public class TypographyTMProStyle
    {
        [field:SerializeField] public float Size {private set; get;}
        [field:SerializeField] public TMP_FontAsset Asset {private set; get;}
        [field:SerializeField] public FontStyles Style {private set; get;}


        public TypographyTMProStyle(float size, TMP_FontAsset asset, FontStyles style) =>
            (Size, Asset, Style) = (size, asset, style);
    }
}