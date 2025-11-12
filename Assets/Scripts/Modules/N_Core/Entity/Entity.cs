using UnityEngine;
using System.Collections.Generic;
using System;
namespace PP3.Core
{
    public sealed class Entity
    {
        private readonly Dictionary<Type, IModule> _mods = new();

        public T Add<T>(T m) where T : IModule { _mods[typeof(T)] = m; m.Init(this); return m; }
        public T Get<T>() where T : class, IModule => _mods.TryGetValue(typeof(T), out var m) ? m as T : null;
        public bool TryGet<T>(out T module) where T : class, IModule { module = Get<T>(); return module != null; }

        public void Tick(float dt) { foreach (var m in _mods.Values) m.Tick(dt); }
        public void Dispose() { foreach (var m in _mods.Values) m.Dispose(); _mods.Clear(); }
    }
}