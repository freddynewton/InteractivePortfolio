using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChapterButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public bool IsFocused { get; set; }

    public void SetVisibility(float amount)
    {
        // DoTween
        _canvasGroup.DOFade(amount, 0.5f);
    }

    private void Awake()
    {
        transform.localScale = Vector3.one * 0.8f;
        _canvasGroup.alpha = 0;
    }

    // Called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Add the logic you want for the click event
        Debug.Log("ChapterButton clicked!");
        // For example, we can set the button to be focused
        IsFocused = true;
        // Maybe animate or change state here
    }

    // Called when the pointer enters the button (hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsFocused)
        {
            SetVisibility(0.5f);
        }

        // You can change the scale or color on hover
        transform.DOScale(1f, 0.1f);  // Scale up on hover
        Debug.Log("Pointer entered ChapterButton");
    }

    // Called when the pointer exits the button (hover)
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsFocused)
        {
            SetVisibility(0);
        }

        // Revert the scale back to normal
        transform.DOScale(0.8f, 0.1f);  // Scale back to original
        Debug.Log("Pointer exited ChapterButton");
    }
}
