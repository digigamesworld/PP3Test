using GameSample.View.MainMenu;
using UnityEngine;

namespace GameSample
{
    public class Initializer : MonoBehaviour
    {
        private void Awake() =>
            MainMenuPanel.Instance.ChangePanel();
    }
}