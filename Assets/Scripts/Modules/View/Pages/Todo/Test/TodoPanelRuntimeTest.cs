using UnityEngine;

namespace GameSample.View.Todos.Test
{
    public class TodoPanelRuntimeTest : MonoBehaviour
    {
        private void Awake() =>
            TodoPanel.Instance.ChangePanel();
    }
}