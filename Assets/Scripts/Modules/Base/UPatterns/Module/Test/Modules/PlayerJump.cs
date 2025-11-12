using UnityEngine;

namespace UPatterns.ModuleTest
{
    public class PlayerJump : PlayerModule
    {
        [SerializeField] private float moveSpeed = 20;

        protected override void ApplyActivate() { }

        protected override void ApplyDeactivate() { }

        public override void Process()
        {
            base.Process();
            owner.Move(Input.GetAxis("Horiozontal") * Vector2.right, moveSpeed);
        }
    }
}