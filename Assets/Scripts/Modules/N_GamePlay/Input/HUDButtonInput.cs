using UnityEngine;

namespace PP3.Core
{
    public sealed class HUDButtonInput : IInputSource
    {
        private InputModule _sink; private float _steer, _throttle, _brake;
        public void SetSteer(float v) { _steer = Mathf.Clamp(v, -1f, 1f); }
        public void SetThrottle(float v) { _throttle = Mathf.Clamp01(v); }
        public void SetBrake(float v) { _brake = Mathf.Clamp01(v); }
        public void PressHorn() { _sink?.Push(new HornCmd(true)); }
        public void Init(Entity e) { _sink = e.Get<InputModule>(); }
        public void Tick(float dt) { _sink?.Push(new SteerCmd(_steer)); _sink?.Push(new ThrottleCmd(_throttle)); if (_brake > 0f) _sink?.Push(new BrakeCmd(_brake)); }
        public void Dispose() { }
    }
}