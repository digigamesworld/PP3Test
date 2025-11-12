using UnityEngine;
using System;

namespace UPatterns
{
    public class Module<T, TSubModule> : MonoBehaviour where T : Module<T, TSubModule> where TSubModule : SubModule<T,TSubModule>
    {
        [SerializeField] private TSubModule[] modules;

        public virtual void Awake() =>
            Initialize();

        public virtual void Initialize()
        {
            for (int i = 0; i < modules.Length; i++)
                modules[i].SetOwner(this as T);
        }

        public virtual void Update()
        {
            for(int i=0;i<modules.Length;i++)
                if(modules[i].IsActive)
                    modules[i].Process();   
        }
        
        public T GetModule<T>() where T : TSubModule
        {
            for (int i = 0; i < modules.Length; i++)
                if (modules[i] is T)
                    return modules[i] as T;
            return null;
        }
    }
}