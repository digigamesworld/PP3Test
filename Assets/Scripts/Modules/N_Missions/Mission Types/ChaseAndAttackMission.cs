using UnityEngine;
using PP3.Missions;
using System;
using PP3.Core;

[CreateAssetMenu(menuName = "PP3/Missions/ChaseAndAttackMission")]
public sealed class ChaseAndAttackMission : MissionSO
{
    public Transform[] Targets; public float FireDistance = 30f; public bool RequireAll = false;
    public override void Begin(in MissionContext ctx)
    {
        base.Begin(in ctx);
        // Ensure weapon module exists
        if (Ctx.Player.Get<WeaponModule>() == null && Ctx.PlayerView != null)
        {
            // Assume you have assigned a projectile prefab in some GameServices
            var muzzle = Ctx.PlayerView; // replace with a real muzzle Transform for accuracy
            Ctx.Player.Add(new WeaponModule(GameConfigs.DefaultWeapon, Ctx.PlayerView));
        }
    }
    public override void Tick(float dt)
    {
        if (Targets == null || Targets.Length == 0 || Ctx.PlayerView == null) return;
        int hitCount = 0;
        foreach (var t in Targets)
        {
            if (t == null) continue;
            float d = Vector3.Distance(Ctx.PlayerView.position, t.position);
            if (d <= FireDistance)
            {
                Vector3 dir = (t.position - Ctx.PlayerView.position).normalized;
                Ctx.Player.Get<InputModule>()?.Push(new FireCmd(dir));
                hitCount++;
            }
        }
        if (RequireAll) IsCompleted = hitCount == Targets.Length; else IsCompleted = hitCount > 0;
    }
    public override void End()
    {
        Log("Mission Ended");
    }
}
