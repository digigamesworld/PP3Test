using UnityEngine;

namespace UStylers
{
    [CreateAssetMenu(fileName = nameof(DefaultStates),menuName = "UStyler/"+ nameof(DefaultStates))]
    public class DefaultStates : ScriptableObject
    {
        private static DefaultStates instance;
        public static DefaultStates Instance =>
            instance ??= Resources.Load<DefaultStates>(nameof(DefaultStates));

        [field:SerializeField] public StateCard Default {private set; get;}
        [field:SerializeField] public StateCard Deactive {private set; get;}
        [field:SerializeField] public StateCard Selected {private set; get;}
        [field:SerializeField] public StateCard Hover {private set; get;}
        [field:SerializeField] public StateCard Success {private set; get;}
        [field:SerializeField] public StateCard Error {private set; get;}
    }
}