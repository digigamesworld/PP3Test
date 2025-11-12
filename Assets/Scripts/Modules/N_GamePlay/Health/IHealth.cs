using System;

namespace PP3.Core
{
    public interface IHealth : IModule
    {
        float Current { get; }
        float Max { get; }

        event Action<float, float> OnChanged;
        event Action OnDied;

        void TakeDamage(float amount, object source = null);
        void Heal(float amount, object source = null);
        void Reset(float? newMax = null);
    }
}
