using UnityEngine;
using System.Collections.Generic;
namespace PP3.Core
{
    public sealed class AIModule : IAgentController
    {
        private readonly Dictionary<string, IBehavior> _behaviors = new();
        private IBehavior _current;
        public void Register(string key, IBehavior b) => _behaviors[key] = b;
        public void SetBehavior(IBehavior b) { _current?.Exit(); _current = b; _current?.Enter(); }
        public void SetBehavior(string key) { if (_behaviors.TryGetValue(key, out var b)) SetBehavior(b); }
        public void Init(Entity e) { }
        public void Tick(float dt) { _current?.Tick(dt); }
        public void Dispose() { _current?.Exit(); _behaviors.Clear(); }
    }
}