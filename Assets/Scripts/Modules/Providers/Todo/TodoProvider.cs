using System;
using GameSample.Entities.Todos;
using UPatterns.Networking;
using UnityEngine.Networking;

namespace GameSample.Provider.Todos
{
    public static class TodoProvider
    {
        private const string BASE_URL = "todos/";

        public static void Get(int id, Action<Todo> callback)
        {
            var req = UHTTP.CreateRequest($"{BASE_URL}{id}", UnityWebRequest.kHttpVerbGET);
            req.Send(() => Response(req));

            void Response(UnityWebRequest req)
            {
                var data = req.GetData<Todo>();
                callback.Invoke(data ?? null);
            }
        }

        public static void GetAll(Action<Todo[]> callback)
        {
            var req = UHTTP.CreateRequest(BASE_URL, UnityWebRequest.kHttpVerbGET);
            req.Send(() => Response(req));

            void Response(UnityWebRequest req)
            {
                var data = req.GetData<Todo[]>();
                callback.Invoke(data ?? null);
            }
        }
    }
}