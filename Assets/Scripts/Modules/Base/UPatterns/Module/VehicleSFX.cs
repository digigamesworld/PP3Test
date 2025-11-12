using UnityEngine;
using PP3.Core;

namespace PP3.Modules.Vehicle
{
    public class VehicleSFX : VehicleSubModule
    {
        private AudioSource horn;

        protected override void ApplyActivate()
        {
            horn = owner.GetComponentInChildren<AudioSource>();

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
            if (cmd is HornCmd h && h.Pressed && horn != null)
                horn.Play();
        }
    }
}
