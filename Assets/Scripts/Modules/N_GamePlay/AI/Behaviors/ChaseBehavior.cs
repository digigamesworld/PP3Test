using System;
using UnityEngine;

namespace PP3.Core
{
    public sealed class ChaseBehavior : IBehavior
    {
        private readonly Entity _self; private readonly Func<Vector3> _targetPos; private readonly Transform _selfTransform;
        private readonly float _aggressiveness; // scales throttle
        public bool IsDone => false;
        public ChaseBehavior(Entity self, Transform selfTransform, Func<Vector3> targetPos, float aggressiveness = 1f) { _self = self; _selfTransform = selfTransform; _targetPos = targetPos; _aggressiveness = Mathf.Clamp01(aggressiveness); }
        public void Enter() { }
        public void Tick(float dt)
        {
            var sink = _self.Get<InputModule>(); if (sink == null) return;
            Vector3 dir = (_targetPos() - _selfTransform.position);
            if (dir.sqrMagnitude < 0.0001f) return;
            dir.Normalize();
            var right = Vector3.Dot(Vector3.Cross(_selfTransform.forward, dir), Vector3.up);
            float steer = Mathf.Clamp(right, -1f, 1f);
            sink.Push(new SteerCmd(steer));
            sink.Push(new ThrottleCmd(_aggressiveness));
        }
        public void Exit() { }
    }
}

