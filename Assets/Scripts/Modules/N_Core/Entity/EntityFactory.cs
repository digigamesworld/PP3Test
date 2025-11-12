using UnityEngine;
using PP3.Gameplay;
using PP3.Modules.Config;
using PP3.Modules.Vehicle;
using PP3.View;
namespace PP3.Core
{

    public static class EntityFactory
    {
        public static Entity MakeVehicle(VehicleDefinitionSO cfg, Transform view, Rigidbody rb, IInputSource inputSource = null)
        {
            var e = new Entity();

            // ===  Core modules ===
            e.Add(new InputModule());
            e.Add(new HealthModule(GameConfigs.DefaultHealth));
            e.Add(new SFXModule(view.GetComponent<AudioSource>()));

            // === 2 Vehicle entity module (bridge to Unity VehicleModule) ===
            var vehEntity = e.Add(new VehicleEntityModule(view, rb, cfg));

            // === 3 Input source (AI / Player / HUD) ===
            e.Add(inputSource ?? new PlayerDeviceInput());

            // === 4 Bind the Unity side ===
            if (view.TryGetComponent(out EntityViewBinder binder))
                binder.Bind(e);

            return e;
        }


  

        // Example placeholder target — replace in your game layer
        private static Vector3 _targetPos = Vector3.zero;
        public static void SetSharedTarget(Vector3 p) => _targetPos = p;
    }
}