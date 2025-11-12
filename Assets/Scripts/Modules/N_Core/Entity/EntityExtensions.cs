using UnityEngine;
using PP3.Core;
namespace PP3.View
{
    /// <summary>
    /// Extension methods for Unity objects to access their PP3 Entity.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Attempts to get the PP3 Entity associated with this GameObject.
        /// </summary>
        public static bool TryGetEntity(this GameObject go, out Entity entity)
        {
            if (go.TryGetComponent(out EntityViewBinder binder) && binder.Entity != null)
            {
                entity = binder.Entity;
                return true;
            }

            entity = null;
            return false;
        }

        /// <summary>
        /// Allows direct calls from Components (MonoBehaviours).
        /// </summary>
        public static bool TryGetEntity(this Component c, out Entity entity)
            => TryGetEntity(c.gameObject, out entity);
    }
}
