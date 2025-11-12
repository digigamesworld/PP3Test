using PP3.Core;
using UnityEngine;

namespace PP3.Core
{
    public interface IAgentController : IModule
    { 
        void SetBehavior(IBehavior b);
    }
}

