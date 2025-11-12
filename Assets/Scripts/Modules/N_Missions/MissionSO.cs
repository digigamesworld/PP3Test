using UnityEngine;
using PP3.Core;

namespace PP3.Missions
{
    public abstract class MissionSO : ScriptableObject, IMission
    {
        [SerializeField] private string displayName = "Mission";
        public virtual string Name => string.IsNullOrEmpty(displayName) ? GetType().Name : displayName;

        protected MissionContext Ctx;
        public bool IsCompleted { get; protected set; }

        public virtual void Begin(in MissionContext ctx)
        {
            Ctx = ctx;
            IsCompleted = false;
        }

        public abstract void Tick(float dt);
        public virtual void End() { }

        protected void Log(string msg) => Ctx.Log?.Invoke($"[{Name}] {msg}");

        // 🧩 NEW: Allow external binding of runtime references
        public virtual void InjectSceneReferences(MissionContext ctx) { }
    }
}
