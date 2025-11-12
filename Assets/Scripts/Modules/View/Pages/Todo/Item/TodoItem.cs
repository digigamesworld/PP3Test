using UnityEngine;
using TMPro;
using GameSample.Entities.Todos;

namespace GameSample.View.Todos
{
    public class TodoItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI idTxt;
        [SerializeField] private TextMeshProUGUI userIdTxt;
        [SerializeField] private TextMeshProUGUI titleTxt;
        [SerializeField] private TextMeshProUGUI completedTxt;


        public void Set(Todo todo)
        {
            idTxt.text = todo.id.ToString();
            userIdTxt.text = todo.userId.ToString();
            titleTxt.text = todo.title;
            completedTxt.text = todo.completed.ToString();
        }
    }
}