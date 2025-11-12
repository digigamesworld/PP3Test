using UnityEngine;

namespace PP3.Modules.Config
{
    [CreateAssetMenu(menuName = "PP3/Configs/VehicleDefinitionSO")]
    public class VehicleDefinitionSO : ScriptableObject
    {
        [Header("Basic Info")]
        [Tooltip("Unique name for this vehicle type (e.g., Police Sedan, SUV, Helicopter).")]
        public string vehicleId = "police_sedan";
        public string displayName = "Police Sedan";

        [Header("Physics")]
        [Tooltip("Mass of the vehicle in kilograms.")]
        public float mass = 1200f;

        [Tooltip("Maximum engine force applied when throttle is full.")]
        public float engineForce = 6000f;

        [Tooltip("Steering torque applied to the Rigidbody when turning.")]
        public float steerTorque = 500f;

        [Tooltip("Maximum linear speed (in meters per second).")]
        public float maxSpeed = 50f;

        [Tooltip("Drag coefficient (air resistance).")]
        public float drag = 0.1f;

        [Tooltip("Angular drag (rotation damping).")]
        public float angularDrag = 0.2f;

        [Header("Suspension / Wheels")]
        [Tooltip("How fast suspension responds to road bumps.")]
        public float suspensionStrength = 12000f;
        [Tooltip("Damping ratio for suspension.")]
        public float suspensionDamping = 1500f;

        [Tooltip("Wheel radius for ground contact points.")]
        public float wheelRadius = 0.35f;

        [Tooltip("Friction for tires (higher = more grip).")]
        public float tireFriction = 1.0f;

        [Header("Health")]
        [Tooltip("Maximum health points for the vehicle.")]
        public float maxHealth = 100f;

        [Tooltip("Regeneration rate per second (optional).")]
        public float regenRate = 0f;

        [Header("Weapon System")]
        [Tooltip("Muzzle transform for projectiles (assigned at runtime).")]
        public Transform muzzle;

        [Tooltip("Projectile prefab fired by this vehicle.")]
        public GameObject projectilePrefab;

        [Tooltip("Projectile launch speed in m/s.")]
        public float projectileSpeed = 60f;

        [Tooltip("Cooldown time between shots (in seconds).")]
        public float fireCooldown = 0.25f;

        [Header("Audio & Visuals")]
        [Tooltip("Audio source prefab or clip for horn.")]
        public AudioClip hornClip;

        [Tooltip("Visual material for the vehicle body.")]
        public Material bodyMaterial;

        [Tooltip("Particle prefab for engine smoke or nitro effect.")]
        public GameObject vfxExhaust;

        [Header("AI Settings")]
        [Tooltip("If true, this vehicle can be used by AI agents.")]
        public bool aiUsable = false;

        [Tooltip("Default AI patrol or chase speed.")]
        public float aiSpeed = 25f;

   
    }


}
