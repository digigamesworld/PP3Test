using UnityEngine;

namespace PP3.Core
{
    public sealed class PlayerDeviceInput : IInputSource
    {
        private InputModule _sink;
        public void Init(Entity e) { _sink = e.Get<InputModule>(); }
        public void Tick(float dt)
        {
           
            _sink?.Push(new SteerCmd(Input.GetAxis("Horizontal")));
            float v = Input.GetAxis("Vertical");
            if (v >= 0f) _sink?.Push(new ThrottleCmd(v)); else _sink?.Push(new BrakeCmd(-v));
            if (Input.GetKeyDown(KeyCode.Space)) _sink?.Push(new HornCmd(true));

        }
        public void Dispose() { }
    }
}