using UnityEngine;

namespace GameSample.View.MainMenu.Test
{
    public class MainMenuPanelRuntimeTest : MonoBehaviour
    {
        private void Awake() =>
            MainMenuPanel.Instance.ChangePanel();
    }
}