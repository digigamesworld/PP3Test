using Unity.Collections;
using Unity.Entities;

public struct EntityDebugName : IComponentData
{
    public FixedString64Bytes Value;
}