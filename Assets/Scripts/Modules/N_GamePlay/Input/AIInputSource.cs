using UnityEngine;
namespace PP3.Core
{
    public sealed class AIInputSource : IInputSource
    {
        private InputModule _sink; private IAgentController _ai;
        public void Init(Entity e) { _sink = e.Get<InputModule>(); _ai = e.Get<IAgentController>(); }
        public void Tick(float dt) { /* AIModule drives commands itself via behaviors; keep this for symmetry */ }
        public void Dispose() { }
    }
}

