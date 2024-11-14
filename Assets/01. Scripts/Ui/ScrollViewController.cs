using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Manages scrolling functionality for a ScrollRect, including alignment and positioning to child elements.
/// </summary>
public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _contentPanel;
    [SerializeField] private Scrollbar _verticalScrollbar;

    [SerializeField] private int _maxWidth = 1300;

    // Vertical layout group for arranging content within the scroll view.
    [SerializeField] private VerticalLayoutGroup _scrollVerticalLayoutGroup;
    /// <summary>
    /// Scrolls the view to focus on a specified child RectTransform within the scrollable content area.
    /// </summary>
    /// <param name="child">The RectTransform of the child element to bring into view.</param>
    public void SnapTo(RectTransform child)
    {
        // Check if it is necessary to scroll to the child element.
        if (child == null || _scrollRect == null || _contentPanel == null)
        {
            return;
        }

        // Check if the child element is already visible within the scroll view.
        if (child.position.y > _scrollRect.content.position.y
            && child.position.y < _scrollRect.content.position.y + _scrollRect.viewport.rect.height)
        {
            return;
        }

        // Update canvases to ensure the latest layout and position.
        Canvas.ForceUpdateCanvases();

        // Calculate the new target position to snap the child element into view.
        Vector2 targetPosition =
        (Vector2)_scrollRect.transform.InverseTransformPoint(_scrollRect.content.position)
            - (Vector2)_scrollRect.transform.InverseTransformPoint(child.position);

        // Use DoTween to smoothly animate the scroll content to the target position.
        _scrollRect.content.DOAnchorPos(new Vector2(_scrollRect.content.anchoredPosition.x, targetPosition.y), 0.5f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                // Ensure the target child is centered in the scroll view.
                _scrollRect.content.anchoredPosition = new Vector2(_scrollRect.content.anchoredPosition.x, targetPosition.y);
            });
    }

    /// <summary>
    /// Configures alignment and spacing settings for the ScrollRect content layout.
    /// </summary>
    private void ConfigureContentLayout()
    {
        // Set alignment of the layout group to top-center for consistent child positioning.
        _scrollVerticalLayoutGroup.childAlignment = TextAnchor.UpperCenter;

        // Set spacing between elements in the scroll view.
        _scrollVerticalLayoutGroup.spacing = 10f;

        // Apply padding to the content layout for top, bottom, left, and right.
        _scrollVerticalLayoutGroup.padding = new RectOffset(20, 20, 10, 10);

        // Ensure a ContentSizeFitter is present and set to fit vertically to preferred size.
        ContentSizeFitter sizeFitter = _scrollVerticalLayoutGroup.GetComponent<ContentSizeFitter>();
        if (sizeFitter == null)
        {
            sizeFitter = _scrollVerticalLayoutGroup.gameObject.AddComponent<ContentSizeFitter>();
        }
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // Set the maximum width of the content childen
        foreach (RectTransform child in _contentPanel)
        {
            if (child.sizeDelta.x > _maxWidth)
            {
                child.sizeDelta = new Vector2(_maxWidth, child.sizeDelta.y);
            }
        }
    }

    /// <summary>
    /// Initializes the layout configuration on awake.
    /// </summary>
    private void Awake()
    {
        ConfigureContentLayout();
    }
}
