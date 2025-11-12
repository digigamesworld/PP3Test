using UnityEngine;
using PP3.Core;
using PP3.Modules.Config;
using PP3.View;

namespace PP3.Modules.Vehicle
{
    /// <summary>
    /// Bridges the modular VehicleModule (MonoBehaviour-based)
    /// with the PP3 Entity system (pure IModule logic).
    /// </summary>
    public sealed class VehicleEntityModule : IModule
    {
        private VehicleModule _vehicle;
        private Transform _view;
        private Rigidbody _rb;
        private VehicleDefinitionSO _cfg;

        public VehicleEntityModule(Transform view, Rigidbody rb, VehicleDefinitionSO cfg)
        {
            _view = view;
            _rb = rb;
            _cfg = cfg;
        }

        public void Init(Entity e)
        {
            // 1️⃣ Ensure VehicleModule exists
            _vehicle = _view.GetComponent<VehicleModule>();

            if (_vehicle == null)
                _vehicle = _view.gameObject.AddComponent<VehicleModule>();
            var attMgr = _view.GetComponent<VehicleAttachmentManager>();
            if (attMgr != null)
                attMgr.InitializeDefaults();
            _vehicle.config = _cfg;
            _vehicle.rb = _rb;

            // 2️⃣ Ensure InputModule exists in Entity
            var inputSink = e.Get<InputModule>() ?? e.Add(new InputModule());

            // 3️⃣ Inject the InputModule into the VehicleModule
            _vehicle.InjectInput(inputSink);

            // 4️⃣ Ensure EntityViewBinder is present
            var binder = _view.GetComponent<EntityViewBinder>() ?? _view.gameObject.AddComponent<EntityViewBinder>();
            binder.Bind(e);

            Debug.LogWarning("VehicleEntityModule initiated");
        }

        public void Tick(float dt)
        {

        }

        public void Dispose() { }
    }
}
