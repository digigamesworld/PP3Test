using UnityEngine;
using PP3.Core;

namespace PP3.Modules.Vehicle
{
    public class VehicleWeapon : VehicleSubModule
    {
        private float cooldownTimer = 0f;

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
            if (cmd is FireCmd f && cooldownTimer <= 0f)
                Fire(f.Direction);
        }

        private void Fire(Vector3 dir)
        {
            cooldownTimer = Config.fireCooldown;
            if (Config.projectilePrefab == null || Config.muzzle == null) return;

            var go = Object.Instantiate(Config.projectilePrefab, Config.muzzle.position, Config.muzzle.rotation);
            if (go.TryGetComponent<Rigidbody>(out var rb))
                rb.AddForce(dir * Config.projectileSpeed, ForceMode.Impulse);
        }

        public override void Process()
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
