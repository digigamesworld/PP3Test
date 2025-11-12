using System;

namespace PP3.Core
{
    public interface IBehavior
    {
        void Enter();
        void Tick(float dt);
        void Exit();
        bool IsDone { get; }
    }
}
