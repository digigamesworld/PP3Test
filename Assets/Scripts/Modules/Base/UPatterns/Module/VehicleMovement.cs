using UnityEngine;
using PP3.Core;

namespace PP3.Modules.Vehicle
{


    namespace PP3.Modules.Vehicle
    {
        public class VehicleMovement : VehicleSubModule
        {
            private float _steer;
            private float _throttle;
            private float _brake;

            protected override void ApplyActivate()
            {
                if (Input != null)
                    Input.OnCommand += OnCommand;
            }

            protected override void ApplyDeactivate()
            {
                if (Input != null)
                    Input.OnCommand -= OnCommand;
            }

            private void OnCommand(ICommand cmd)
            {
                switch (cmd)
                {
                    case SteerCmd s: _steer = s.Value; break;
                    case ThrottleCmd t: _throttle = t.Value; break;
                    case BrakeCmd b: _brake = b.Value; break;
                }
            
            }

            public override void Process()
            {
                if (Rb == null || Config == null) return;

                float accel = _throttle * Config.engineForce - _brake * Config.engineForce * 0.5f;

                Rb.AddForce(owner.transform.forward * accel * Time.deltaTime, ForceMode.Acceleration);
                Rb.AddTorque(Vector3.up * _steer * Config.steerTorque * Time.deltaTime, ForceMode.Acceleration);
 
            }
        }
    }

}
