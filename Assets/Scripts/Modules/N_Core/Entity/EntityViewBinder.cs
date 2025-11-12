using UnityEngine;
using PP3.Core;
namespace PP3.View
{
    /// <summary>
    /// MonoBehaviour bridge between a Unity GameObject and a pure PP3 Entity.
    /// Allows lookup of the Entity from collisions, triggers, etc.
    /// </summary>
    [DisallowMultipleComponent]
    public class EntityViewBinder : MonoBehaviour
    {
        public Entity Entity { get; private set; }

        /// <summary>
        /// Called when the Entity is created or composed at runtime.
        /// </summary>
        public void Bind(Entity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Optional: clears the entity reference (e.g. on despawn).
        /// </summary>
        public void Unbind()
        {
            Entity = null;
        }
    }
}
