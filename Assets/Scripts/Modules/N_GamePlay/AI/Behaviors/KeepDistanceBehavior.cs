using UnityEngine;
using System;
namespace PP3.Core
{
    public sealed class KeepDistanceBehavior : IBehavior
    {
        private readonly Entity _self; private readonly Transform _selfTransform; private readonly Func<Vector3> _targetPos; private readonly float _radius;
        public bool IsDone => false;
        public KeepDistanceBehavior(Entity self, Transform selfTransform, Func<Vector3> targetPos, float radius) { _self = self; _selfTransform = selfTransform; _targetPos = targetPos; _radius = radius; }
        public void Enter() { }
        public void Tick(float dt)
        {
            var sink = _self.Get<InputModule>(); if (sink == null) return;
            var delta = _targetPos() - _selfTransform.position; var dist = delta.magnitude; var dir = delta.normalized;
            float steer = Mathf.Clamp(Vector3.SignedAngle(_selfTransform.forward, dir, Vector3.up) / 45f, -1f, 1f);
            float throttle = Mathf.Clamp01(Mathf.Abs(dist - _radius) / _radius);
            sink.Push(new SteerCmd(steer));
            sink.Push(new ThrottleCmd(throttle));
        }
        public void Exit() { }
    }
}
