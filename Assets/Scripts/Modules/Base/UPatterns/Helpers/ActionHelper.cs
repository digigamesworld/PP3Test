using System;
using UnityEngine;

namespace UPatterns
{
    public static class ActionHelper
    {
        public static void CallbacksMerge<T>(this T[] items,Action<T,Action<bool>> validate,Action<bool> callbackFinal) =>
            CallbacksMerge(items,validate,(value,item) => callbackFinal?.Invoke(value));
            
        public static void CallbacksMerge<T>(this T[] items,Action<T,Action<bool>> validate,Action<bool,T> callbackFinal)
        {
            int count = 0;
            bool failed = false;

            items.ForEach(item =>  validate?.Invoke(item,value => Callback(value,item)));

            void Callback(bool value,T item) 
            {
                if (failed) return;

                if (!value)
                {
                    callbackFinal?.Invoke(false,item);
                    Debug.Log(item+": Failed ------------------------------------------------------------------------");

                    failed = true;
                    return;
                }

                count++;
                if (count == items.Length)
                { 
                    Debug.Log("Success-----------------------------------------------------------------------------");
                    callbackFinal?.Invoke(true, item); }
            }
        }
    }
}