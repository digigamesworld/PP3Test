using UnityEngine;
using PP3.Core;

namespace PP3.Missions
{
    [CreateAssetMenu(menuName = "PP3/Missions/Reach Point")]
    public class ReachPointMissionSO : MissionSO
    {
        [Header("Runtime Bindings")]
        [HideInInspector] public Transform TargetPoint;

        [Header("Parameters")]
        public float Radius = 5f;
        public bool StopOnArrive = true;

        public override void InjectSceneReferences(MissionContext ctx)
        {
            // If Ctx provides a shared target (e.g., set by GameComposer)
            if (TargetPoint == null && ctx.GetSharedTarget != null)
                TargetPoint = ctx.GetSharedTarget.Invoke();
        }

        public override void Tick(float dt)
        {
            if (Ctx.PlayerView == null || TargetPoint == null)
                return;

            float distance = Vector3.Distance(Ctx.PlayerView.position, TargetPoint.position);

            if (distance <= Radius)
            {
                if (StopOnArrive && Ctx.PlayerRb != null)
                {
                    Ctx.PlayerRb.linearVelocity = Vector3.zero;
                    Ctx.PlayerRb.angularVelocity = Vector3.zero;
                }

                IsCompleted = true;
                Log("Reached target");
            }
        }

        public override void End()
        {
            Log("Mission Ended");
        }
    }
}
