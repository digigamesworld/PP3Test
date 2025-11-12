
using UnityEngine;

namespace PP3.Core
{
    public class VFXModule : IModule
    {
        private readonly Transform parent;
        private readonly VFXConfigs config;

        public VFXModule(Transform parentTransform, VFXConfigs cfg)
        {
            parent = parentTransform;
            config = cfg;
        }

        public void Init(Entity e) { }
        public void Tick(float dt) { }
        public void Dispose() { }

        /// <summary>
        /// Spawns an effect by ID from the VFXConfig.
        /// </summary>
        public void Spawn(string id, Vector3 position, Quaternion rotation, float scale = 1f)
        {
            if (!config.TryGet(id, out var fx) || fx.prefab == null)
                return;

            var instance = Object.Instantiate(fx.prefab, position, rotation, parent);
            instance.transform.localScale *= scale;
            if (fx.lifetime > 0)
                Object.Destroy(instance, fx.lifetime);
        }
    }
}

