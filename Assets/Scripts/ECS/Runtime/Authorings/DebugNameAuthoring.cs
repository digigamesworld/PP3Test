using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class DebugNameAuthoring : MonoBehaviour
{
    class Baker : Baker<DebugNameAuthoring>
    {
        public override void Bake(DebugNameAuthoring authoring)
        {
            var e = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(e, new EntityDebugName { Value = authoring.name });
        }
    }
}
