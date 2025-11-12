using UnityEngine;
using UPatterns.Networking;

namespace GameSample
{
    public static class APIConfig
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            UHTTP.Config = new("https://jsonplaceholder.typicode.com/");
        }
    }
}