using UnityEngine;

namespace UPatterns.ModuleTest
{
    public class PlayerMove : PlayerModule
    {
        private const string VERTICAL_AXIS = "Vertical";
        private const string HORIZONTAL_AXIS = "Horizontal";

        [SerializeField] private float moveSpeed = 20;

        private Vector2 move = Vector2.zero;

        protected override void ApplyActivate() { }

        protected override void ApplyDeactivate() { }

        public override void Process()
        {
            base.Process();
            move.x = Input.GetAxis(HORIZONTAL_AXIS);
            move.y = Input.GetAxis(VERTICAL_AXIS);
            owner.Move(move, moveSpeed);
        }
    }
}