using PP3.Modules.Vehicle;
using UnityEngine;

[System.Serializable]
public class VehicleAttachment
{
    public string id;
    public PartSlotId slot;
    public GameObject prefab;
    public bool enabled = true;
}
