using System;
using UPatterns;

namespace PP3.Data
{
    [Serializable]
    public record WeaponProfileState : UState
    {
        public string weaponId { get; init; }
        public bool isUnlocked { get; init; }
        public int level { get; init; }
        public int ammoCount { get; init; }
    }
}
