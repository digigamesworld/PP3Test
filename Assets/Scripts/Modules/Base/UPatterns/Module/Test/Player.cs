using UnityEngine;

namespace UPatterns.ModuleTest
{
    public class Player : Module<Player, PlayerModule>
    {
        private Transform tr;

        public override void Awake()
        {
            base.Awake();

            tr = transform;

            GetModule<PlayerMove>().DoActivate();
        }

        public void Move(Vector2 dir, float speed) =>
            tr.position += (Vector3)dir * speed * Time.deltaTime;

    }
}