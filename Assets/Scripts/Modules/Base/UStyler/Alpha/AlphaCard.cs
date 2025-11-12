using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(AlphaCard), menuName = "UStyler/"+ nameof(AlphaCard), order = 0)]
    public class AlphaCard : StyleCard<float>
    {
        [SerializeField] private float value;
        public override float Value => value;
    }
}