using System;
using System.Collections.Generic;
using UPatterns;

namespace PP3.Data
{
    [Serializable]
    public record PlayerProfileState : UState
    {
        public string playerId { get; init; }
        public string playerName { get; init; }

        public string selectedVehicleId { get; init; }
        public string selectedWeaponId { get; init; }

        public int level { get; init; }
        public int currency { get; init; }
        public int experience { get; init; }

        // Garage and arsenal
        public Dictionary<string, VehicleProfileState> vehicles { get; init; } = new();
        public Dictionary<string, WeaponProfileState> weapons { get; init; } = new();
    }
}
