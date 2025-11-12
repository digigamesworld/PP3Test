using UnityEngine;
using System;
namespace PP3.Core
{
    public sealed class AttackBehavior : IBehavior
    {
        private readonly Entity _self; private readonly Action _fire;
        public bool IsDone => false;
        public AttackBehavior(Entity self, Action fire) { _self = self; _fire = fire; }
        public void Enter() { }
        public void Tick(float dt) { _fire?.Invoke(); }
        public void Exit() { }
    }
}
