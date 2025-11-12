using PP3.Core;
using UnityEngine;
using PP3.Gameplay.Combat;
namespace PP3.Missions
{
    public class WeaponModule : IModule
    {
        private WeaponConfigSO config;
        private Transform firePoint;
        private float cooldown;
        private int currentAmmo;
        private SFXModule sfx;
        private VFXModule vfx;

        public WeaponModule(WeaponConfigSO cfg, Transform muzzle, SFXModule sfxMod = null, VFXModule vfxMod = null)
        {
            config = cfg;
            firePoint = muzzle;
            sfx = sfxMod;
            vfx = vfxMod;
        }

        public void Init(Entity e)
        {
            currentAmmo = config.maxAmmo;
            cooldown = 0;
        }

        public void Tick(float dt)
        {
            cooldown -= dt;
        }

        public void Dispose() { }

        public bool TryFire(Entity owner)
        {
            if (cooldown > 0f || currentAmmo <= 0) return false;

            cooldown = 1f / config.fireRate;
            currentAmmo--;

            if (config.projectilePrefab)
            {
                var proj = Object.Instantiate(config.projectilePrefab, firePoint.position, firePoint.rotation);
                if (proj.TryGetComponent(out ProjectileBehaviour pb))
                    pb.Init(config.damage, owner);
            }

            // Play effects
            sfx?.Play("fire");
            if (config.muzzleFlashVfx && vfx != null)
                vfx.Spawn("muzzle", firePoint.position, Quaternion.identity);

            return true;
        }

        public void Reload() => currentAmmo = config.maxAmmo;
    }
}

