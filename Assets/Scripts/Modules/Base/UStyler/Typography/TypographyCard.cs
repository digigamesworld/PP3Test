using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(TypographyCard), menuName = "UStyler/"+nameof(TypographyCard), order = 0)]
    public class TypographyCard : StyleCard<TypographyStyle>
    {
        [SerializeField] private int size = 14;
        [SerializeField] private Font asset;
        [SerializeField] private FontStyle style;
        public override TypographyStyle Value => new(size, asset, style);
    }

    [System.Serializable]
    public class TypographyStyle
    {
        [field:SerializeField] public int Size {private set; get;}
        [field:SerializeField] public Font Asset {private set; get;}
        [field:SerializeField] public FontStyle Style {private set; get;}

        public TypographyStyle(int size, Font asset, FontStyle style) =>
            (Size, Asset, Style) = (size, asset, style);
    }
}