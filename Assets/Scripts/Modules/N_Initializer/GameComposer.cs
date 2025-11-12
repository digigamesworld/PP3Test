using UnityEngine;
using PP3.Core;
using PP3.Missions;
using PP3.Modules.Vehicle;

public class GameComposer : MonoBehaviour
{
    [Header("Player Vehicle")]
    public VehicleModule playerVehicle;

    [Header("Mission Setup")]
    public ReachPointMissionSO missionAsset;
    public Transform reachTarget;

    private Entity playerEntity;
    private MissionManager missionManager;

    void Start()
    {
        // === 1️⃣ Validate references ===
        if (playerVehicle == null || missionAsset == null)
        {
            Debug.LogError("[GameComposer] Missing vehicle or mission asset!");
            return;
        }

        // === 2️⃣ Build player entity ===
        var rb = playerVehicle.GetComponent<Rigidbody>();
        playerEntity = EntityFactory.MakeVehicle(
            playerVehicle.config,
            playerVehicle.transform,
            rb,
            new PlayerDeviceInput()
        );

        // === 3️⃣ Add and initialize SimpleMissionManager ===
        missionManager = gameObject.AddComponent<MissionManager>();
        missionManager.playerEntity = playerEntity;
        missionManager.playerView = playerVehicle.transform;
        missionManager.playerRb = rb;
        missionManager.mission = missionAsset;

        // Optionally set target for ReachPoint mission
        if (missionAsset is ReachPointMissionSO reachMission && reachTarget != null)
            reachMission.TargetPoint = reachTarget;
    }

    void Update()
    {
        playerEntity?.Tick(Time.deltaTime);
    }

    void OnDestroy()
    {
        playerEntity?.Dispose();
    }
}
