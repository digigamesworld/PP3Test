using Unity.Entities;
using UnityEngine;
namespace  PP3.Core
{
    public interface IModule
    {
        void Init(Entity e);
        void Tick(float dt);
        void Dispose(); 
    }

}
