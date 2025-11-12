using PP3.Modules.Vehicle;
using UnityEngine;


public enum PartSlotId : byte
{
    Spoiler, DoorFL, DoorFR, DoorBL, DoorBR, Hood, RoofLight, WeaponMount
}
public class PartSlot : MonoBehaviour
{
    public PartSlotId id;
    public Transform mountPoint; // default to self if null
    private Transform cached => mountPoint != null ? mountPoint : transform;

    public Transform GetMountPoint() => cached;
}
