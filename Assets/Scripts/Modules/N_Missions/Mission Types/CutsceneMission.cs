using UnityEngine;
using PP3.Missions;
using System;


[CreateAssetMenu(menuName = "PP3/Missions/CutsceneMission")]
public sealed class CutsceneMission : MissionSO
{
    public float Duration = 3f; public bool FreezePlayer = true;
    private float _t;
    public override void Begin(in MissionContext ctx) { base.Begin(in ctx); _t = 0f; if (FreezePlayer) Ctx.SoftPause?.Invoke(true); }
    public override void Tick(float dt) { _t += dt; if (_t >= Duration) { IsCompleted = true; } }
    public override void End() { if (FreezePlayer) Ctx.SoftPause?.Invoke(false); }
}

