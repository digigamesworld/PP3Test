using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(ColorCard), menuName = "UStyler/"+ nameof(ColorCard), order = 0)]
    public class ColorCard : StyleCard<Color>
    {
        [field: SerializeField] public Color Color { private set; get; }
        
        public override Color Value => Color;
    }
}