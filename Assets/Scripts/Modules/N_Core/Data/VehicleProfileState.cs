using System;
using System.Collections.Generic;
using PP3.Modules.Vehicle;
using UPatterns;

namespace PP3.Data
{
    [Serializable]
    public record VehicleProfileState : UState
    {
        public string vehicleId { get; init; }
        public string blueprintId { get; init; }
        public string skinId { get; init; }

        // slot → prefab or addressable key
        public Dictionary<PartSlotId, string> equippedAttachments { get; init; } = new();

        // slot → list of owned attachment IDs
        public Dictionary<PartSlotId, List<string>> ownedAttachments { get; init; } = new();

        // optional tuning
        public float engineLevel { get; init; } = 1f;
        public float brakeLevel { get; init; } = 1f;
    }
}
