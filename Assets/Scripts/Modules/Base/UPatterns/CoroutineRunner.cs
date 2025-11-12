using System;
using System.Collections;
using UnityEngine;

namespace UPatterns
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner instance;
        private static CoroutineRunner Instance =>
            instance ??= new GameObject(nameof(CoroutineRunner)).AddComponent<CoroutineRunner>();

        public static Coroutine Run(IEnumerator func) =>
            Instance.StartCoroutine(func);

        public static void Stop(Coroutine coroutine) =>
            Instance.StopCoroutine(coroutine);

        public static Coroutine Run(Action action, float delay = 1) =>
            Run(ActionCaller(action, delay));

        public static IEnumerator ActionCaller(Action action, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            action?.Invoke();
        }

        private void OnDestroy() =>
            instance = null;
    }
}