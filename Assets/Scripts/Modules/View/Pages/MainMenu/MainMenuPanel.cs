using UnityEngine;
using UnityEngine.UI;
using UPatterns;
using GameSample.View.Todos;

namespace GameSample.View.MainMenu
{
    public class MainMenuPanel : UPanel<MainMenuPanel>
    {
        [SerializeField] private Button showTodosBtn;

        private void Awake() =>
            showTodosBtn.onClick.AddListener(ShowTodos);

        private void ShowTodos() =>
            TodoPanel.Instance.ChangePanel();
    }
}