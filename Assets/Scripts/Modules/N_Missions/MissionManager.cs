using UnityEngine;
using PP3.Core;

namespace PP3.Missions
{
    public class MissionManager : MonoBehaviour
    {
        [Header("Setup")]
        public Entity playerEntity;
        public Transform playerView;
        public Rigidbody playerRb;
        public MissionSO mission;    // e.g. ReachPointMissionSO asset

        private bool _running;

        void Start()
        {
            if (mission == null || playerEntity == null || playerView == null || playerRb == null)
            {
                Debug.LogError("[SimpleMissionManager] Missing references!");
                return;
            }

            // build context
            var ctx = new MissionContext
            {
                Player = playerEntity,
                PlayerView = playerView,
                PlayerRb = playerRb,
                Log = Debug.Log
            };

            // start mission
            mission.Begin(ctx);
            _running = true;
            Debug.Log($"[SimpleMissionManager] Started mission: {mission.Name}");
        }

        void Update()
        {
            if (!_running) return;

            mission.Tick(Time.deltaTime);

            if (mission.IsCompleted)
            {
                mission.End();
                _running = false;
                Debug.Log("[SimpleMissionManager] Mission completed!");
            }
        }
    }
}
