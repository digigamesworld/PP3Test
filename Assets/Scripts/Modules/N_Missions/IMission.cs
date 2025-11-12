using UnityEngine;
using System;
using PP3.Core;
namespace PP3.Missions
{
    public struct MissionContext
    {
        public Entity Player;
        public Transform PlayerView;
        public Rigidbody PlayerRb;
        public Action<bool> SoftPause;
        public Func<Transform> GetCameraAnchor;
        public Func<Transform> GetSharedTarget;
        public Func<Transform> GetTargetTransform;
        public Action<Entity, Transform, Rigidbody> OnPlayerChanged;
        public Action<string> Log;
    }

    public interface IMission
    {
        string Name { get; }
        bool IsCompleted { get; }
        void Begin(in MissionContext ctx);
        void Tick(float dt);
        void End();
    }
}