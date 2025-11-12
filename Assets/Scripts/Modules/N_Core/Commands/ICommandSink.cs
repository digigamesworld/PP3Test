using UnityEngine;
namespace PP3.Core
{
    public interface ICommandSink { void Push(ICommand cmd); }
}

