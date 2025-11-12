
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomScrollView : MonoBehaviour, IEndDragHandler
{
    public ScrollRect scrollRect;
    public float friction = 0.9f; // Adjust friction (0.95 for smoother, 0.85 for faster stop)

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        scrollRect.verticalNormalizedPosition =0;
        // StartCoroutine(SmoothStop());
    }

    private System.Collections.IEnumerator SmoothStop()
    {
        while (scrollRect.velocity.magnitude > 10f) // Only slow down if moving
        {
            scrollRect.velocity *= friction; // Apply gradual stop
            yield return null;
        }
        scrollRect.velocity = Vector2.zero; // Fully stop
    }
}