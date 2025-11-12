#if UNITY_EDITOR
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[WorldSystemFilter(WorldSystemFilterFlags.Editor)]
public partial class VehicleGizmoCollectorSystem : SystemBase
{
    public struct VehicleData
    {
        public float3 Position;
        public FixedString64Bytes Name;
        public Entity Entity;
    }

    // Public, read-only “buffer” the overlay will read each frame
    public static NativeList<VehicleData> Vehicles;

    protected override void OnCreate()
    {
        Vehicles = new NativeList<VehicleData>(Allocator.Persistent);
    }

    protected override void OnDestroy()
    {
        if (Vehicles.IsCreated) Vehicles.Dispose();
    }

    protected override void OnUpdate()
    {
        if (!Vehicles.IsCreated) return;
        Vehicles.Clear();

        // Collect ALL entities that have a name + transform (feel free to add a VehicleTag if you want)
        foreach (var (name, transform, entity)
                 in SystemAPI.Query<RefRO<EntityDebugName>, RefRO<LocalTransform>>().WithEntityAccess())
        {
            Vehicles.Add(new VehicleData
            {
                Position = transform.ValueRO.Position,
                Name = name.ValueRO.Value,
                Entity = entity
            });
        }
    }
}
#endif
