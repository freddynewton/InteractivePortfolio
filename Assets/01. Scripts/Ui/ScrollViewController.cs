using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages scrolling functionality for a ScrollRect, including alignment and positioning to child elements.
/// </summary>
public class ScrollViewController : MonoBehaviour
{
    // Reference to the ScrollRect controlling the scrolling behavior.
    [SerializeField] private ScrollRect _scrollView;

    // Vertical layout group for arranging content within the scroll view.
    [SerializeField] private VerticalLayoutGroup _scrollVerticalLayoutGroup;

    /// <summary>
    /// Scrolls the view to focus on a specified child RectTransform within the scrollable content area.
    /// </summary>
    /// <param name="child">The RectTransform of the child element to bring into view.</param>
    public void ScrollToChild(RectTransform child)
    {
        // Ensure the layout updates before calculating position.
        Canvas.ForceUpdateCanvases();

        // Calculate the position of the child element relative to the scroll view.
        Vector2 childPosition = child.position;

        // Calculate the position of the child element relative to the scroll view's content.
        Vector2 childLocalPosition = _scrollView.content.InverseTransformPoint(childPosition);

        // Calculate the position of the child element relative to the scroll view's viewport.
        Vector2 viewportLocalPosition = _scrollView.viewport.InverseTransformPoint(childPosition);

        // Calculate the offset needed to center the child element within the scroll view.
        Vector2 offset = viewportLocalPosition - childLocalPosition;

        // Apply the offset to the scroll view's normalized position.
        _scrollView.normalizedPosition += offset / _scrollView.content.rect.size.y;
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
    }

    /// <summary>
    /// Initializes the layout configuration on awake.
    /// </summary>
    private void Awake()
    {
        ConfigureContentLayout();
    }
}
