using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the visibility and alignment for the readable portfolio canvas.
/// </summary>
public class ReadablePortfolioCanvasController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private ScrollRect _scrollView;
    [SerializeField] private VerticalLayoutGroup _scrollVerticalLayoutGroup;

    private ReadablePortfolioAnimator _readablePortfolioAnimator;

    /// <summary>
    /// Sets the visibility of the portfolio canvas.
    /// </summary>
    public void SetVisibility(bool isVisible)
    {
        _readablePortfolioAnimator.SetVisibility(isVisible);
    }

    /// <summary>
    /// Configures alignment and spacing for the ScrollRect content layout.
    /// </summary>
    private void ConfigureContentLayout()
    {
        // Ensure the VerticalLayoutGroup settings are configured for top alignment
        _scrollVerticalLayoutGroup.childAlignment = TextAnchor.UpperCenter;
        _scrollVerticalLayoutGroup.spacing = 10f;  // Set spacing between items
        _scrollVerticalLayoutGroup.padding = new RectOffset(20, 20, 10, 10); // Top, bottom, left, right padding

        // Ensure ContentSizeFitter is correctly set to fit child elements
        ContentSizeFitter sizeFitter = _scrollVerticalLayoutGroup.GetComponent<ContentSizeFitter>();
        if (sizeFitter == null)
        {
            sizeFitter = _scrollVerticalLayoutGroup.gameObject.AddComponent<ContentSizeFitter>();
        }
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    private void Awake()
    {
        _readablePortfolioAnimator = new ReadablePortfolioAnimator(_canvasGroup);
        _readablePortfolioAnimator.SetVisibility(false);
        ConfigureContentLayout();
    }
}
