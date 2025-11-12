using PP3.Core;
using PP3.Modules.Config;
using PP3.Modules.Vehicle.PP3.Modules.Vehicle;
using UnityEngine;
using UPatterns;
using UPatterns.ModuleTest;

public class VehicleModule : Module<VehicleModule, VehicleSubModule>
{
    [Header("Vehicle Components")]
    public Rigidbody rb;
    public VehicleDefinitionSO config;

    private InputModule _inputSink;

    public void InjectInput(InputModule sink)
    {
        _inputSink = sink;
        GetModule<VehicleMovement>().DoActivate();
    }

    public InputModule GetInputSink() => _inputSink;

    public override void Awake()
    {
        base.Awake();
        rb ??= GetComponent<Rigidbody>();
       
    }
}
