using UnityEngine;
using PP3.Core;
using PP3.View;   // 🟢 Add this — contains EntityExtensions (TryGetEntity)

namespace PP3.Gameplay.Combat
{
    /// <summary>
    /// Simple projectile behaviour for PP3.
    /// Handles forward motion and applies damage on collision.
    /// </summary>
    public class ProjectileBehaviour : MonoBehaviour
    {
        private float damage;
        private Entity owner;

        [Header("Projectile Settings")]
        public float speed = 100f;
        public float lifetime = 5f;

        public void Init(float dmg, Entity src)
        {
            damage = dmg;
            owner = src;
            Destroy(gameObject, lifetime);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision hit)
        {
            // 🟢 Use our new extension to map GameObject → PP3 Entity
            if (hit.gameObject.TryGetEntity(out var target) && target != owner)
            {
                if (target.TryGet(out IHealth health))
                    health.TakeDamage(damage, owner);
            }

            Destroy(gameObject);
        }
    }
}
