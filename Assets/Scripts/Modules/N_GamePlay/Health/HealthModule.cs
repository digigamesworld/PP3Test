using UnityEngine;
using System;
using PP3.Core;

namespace PP3.Gameplay
{
    public sealed class HealthModule : IHealth
    {
        private HealthData _data;
        private Entity _entity;

        public float Current => _data.Current;
        public float Max => _data.Max;

        public event Action<float, float> OnChanged;
        public event Action OnDied;

        public HealthModule(HealthConfig config)
        {
            _data = new HealthData(config.Max);
        }

        public void Init(Entity e)
        {
            _entity = e;
            _data.Current = _data.Max;
            OnChanged?.Invoke(_data.Current, _data.Max);
        }

        public void Tick(float dt) { }

        public void Dispose() { }

        public void TakeDamage(float amount, object src = null)
        {
            _data.Current = Mathf.Clamp(_data.Current - amount, 0, _data.Max);
            OnChanged?.Invoke(_data.Current, _data.Max);

            if (_data.Current <= 0)
            {
                OnDied?.Invoke();
                Debug.Log($"{_entity} destroyed by {src}");
            }
        }

        public void Heal(float amount, object src = null)
        {
            _data.Current = Mathf.Min(_data.Max, _data.Current + amount);
            OnChanged?.Invoke(_data.Current, _data.Max);
        }

        public void Reset(float? newMax = null)
        {
            if (newMax.HasValue) _data.Max = newMax.Value;
            _data.Current = _data.Max;
            OnChanged?.Invoke(_data.Current, _data.Max);
        }

        public HealthData GetData() => _data;
    }
}
