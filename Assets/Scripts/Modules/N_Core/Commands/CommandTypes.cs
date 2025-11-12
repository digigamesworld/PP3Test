// FILE: PP3/Core/CommandTypes.cs
using UnityEngine;

namespace PP3.Core
{
    // Ground vehicle controls
    public readonly struct SteerCmd : ICommand { public readonly float Value; public SteerCmd(float v) { Value = v; } }
    public readonly struct ThrottleCmd : ICommand { public readonly float Value; public ThrottleCmd(float v) { Value = v; } }
    public readonly struct BrakeCmd : ICommand { public readonly float Value; public BrakeCmd(float v) { Value = v; } }
    public readonly struct HornCmd : ICommand { public readonly bool Pressed; public HornCmd(bool p) { Pressed = p; } }

    // Air vehicle controls
    public readonly struct PitchCmd : ICommand { public readonly float Value; public PitchCmd(float v) { Value = v; } }
    public readonly struct RollCmd : ICommand { public readonly float Value; public RollCmd(float v) { Value = v; } }
    public readonly struct YawCmd : ICommand { public readonly float Value; public YawCmd(float v) { Value = v; } }

    // Weapons & abilities
    public readonly struct FireCmd : ICommand { public readonly Vector3 Direction; public FireCmd(Vector3 dir) { Direction = dir; } }
    public readonly struct AbilityCmd : ICommand { public readonly int Slot; public readonly bool Pressed; public AbilityCmd(int slot, bool pressed) { Slot = slot; Pressed = pressed; } }
}
