
using UnityEngine;
namespace PP3.Core
{
    /// <summary>
    /// Central access point for all global ScriptableObject configs in PP3.
    /// </summary>
    public static class GameConfigs
    {
        private static bool initialized;
        private static WeaponConfigSO _defaultWeapon;
        private static WeaponConfigSO _shotgunWeapon;
        public static WeaponConfigSO ShotgunWeapon
        {
            get
            {
                if (_shotgunWeapon == null)
                    _shotgunWeapon = Resources.Load<WeaponConfigSO>("Configs/ShotgunWeapon");
                return _shotgunWeapon;
            }
        }
        private static VFXConfigs _defaultVFX;
        private static SFXConfig _defaultSFX;
        private static HealthConfig _defaultHealth;
        public static void Init()
        {
            if (initialized) return;

            // You can store your configs in a "Resources/Configs/" folder for simplicity.
            _defaultWeapon = Resources.Load<WeaponConfigSO>("Configs/DefaultWeapon");
            _defaultVFX = Resources.Load<VFXConfigs>("Configs/DefaultVFX");
            _defaultSFX = Resources.Load<SFXConfig>("Configs/DefaultSFX");
            _defaultHealth = Resources.Load<HealthConfig>("Configs/DefaultHealth");
            initialized = true;
        }

        public static WeaponConfigSO DefaultWeapon
        {
            get
            {
                if (_defaultWeapon == null) Init();
                return _defaultWeapon;
            }
        }

        public static VFXConfigs DefaultVFX
        {
            get
            {
                if (_defaultVFX == null) Init();
                return _defaultVFX;
            }
        }

        public static SFXConfig DefaultSFX
        {
            get
            {
                if (_defaultSFX == null) Init();
                return _defaultSFX;
            }
        }
        public static HealthConfig DefaultHealth
        {
            get
            {
                if (_defaultHealth == null) Init();
                return _defaultHealth;
            }
        }
    }
}
