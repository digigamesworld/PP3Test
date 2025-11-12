using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(GradientCard), menuName = "UStyler/"+nameof(GradientCard), order = 0)]
    public class GradientCard : StyleCard<GradientStyle>
    {
        [field: SerializeField] public Color Color1 { private set; get; }
        [field: SerializeField] public Color Color2 { private set; get; }

        public override GradientStyle Value => new(Color1, Color2);
    }

    [System.Serializable]
    public class GradientStyle
    {
        [field:SerializeField] public Color Color1 {private set; get;}
        [field:SerializeField] public Color Color2 {private set; get;}

        public GradientStyle(Color color1, Color color2) =>
            (Color1, Color2) = (color1,color2);
    }
}