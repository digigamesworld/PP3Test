using GameSample.Entities.Todos;
using UnityEngine;
using UPatterns;
using UPatterns.Networking;

namespace GameSample.Provider.Todos.Test
{
    public class TodoProviderRuntimeTest : MonoBehaviour
    {
        [SerializeField] private int id = 10;

        [ContextMenu(nameof(GetAll))]
        public void GetAll()
        {
            TodoProvider.GetAll(Set);

            void Set(Todo[] todos)
            {
                if (todos == null || todos.Length == 0)
                {
                    Debug.Log("Todo Length == 0");
                    return;
                }

                todos.ForEach(todo => print(todo));
            }
        }

        [ContextMenu(nameof(GetById))]
        public void GetById()
        {
            TodoProvider.Get(id, Set);

            void Set(Todo todo)
            {
                if (todo == null)
                {
                    Debug.Log("Todo is null");
                    return;
                }

                print(todo);
            }
        }
    }
}
