using PP3.Core;
using PP3.Modules.Config;
using PP3.Modules.Vehicle;
using UnityEngine;
using UPatterns;

    public class VehicleSubModule : SubModule<VehicleModule, VehicleSubModule>
    {
    protected InputModule Input => owner.GetInputSink();
    protected Rigidbody Rb => owner.rb;
    protected VehicleDefinitionSO Config => owner.config;
    protected override void ApplyActivate()
        {

        }

        protected override void ApplyDeactivate()
        {

        }
    }

