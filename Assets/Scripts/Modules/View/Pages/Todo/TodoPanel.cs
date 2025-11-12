using GameSample.Entities.Todos;
using GameSample.Provider.Todos;
using UnityEngine;
using UPatterns;

namespace GameSample.View.Todos
{
    public class TodoPanel : UPanelDataLoader<TodoPanel>
    {
        [SerializeField] private Pool<TodoItem> itemPool;

        private Todo[] todos;

        public override void Initialize() { }

        public override void FetchData()
        {
            base.FetchData();

            itemPool.DeactiveAllInstance();
            TodoProvider.GetAll(Set);

            void Set(Todo[] todos)
            {
                if (todos == null)
                {
                    SetState(PanelState.Error);
                    return;
                }

                this.todos = todos;
                SetState(PanelState.Data);
            }
        }

        public override void OnLoadData()
        {
            base.OnLoadData();

            if (todos == null || todos.Length == 0)
                return;

            for (int i = 0; i < todos.Length; i++)
                itemPool.GetActive.Set(todos[i]);
        }
    }
}