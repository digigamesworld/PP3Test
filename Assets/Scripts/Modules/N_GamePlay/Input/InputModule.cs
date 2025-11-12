using UnityEngine;
using System;

namespace PP3.Core
{
    public sealed class InputModule : IModule, ICommandSink
    {
        public event Action<ICommand> OnCommand; // observers: locomotion / sfx / abilities
        public void Init(Entity e) { }
        public void Tick(float dt) { }
        public void Dispose() { OnCommand = null; }
        public void Push(ICommand c) => OnCommand?.Invoke(c);
    }
}