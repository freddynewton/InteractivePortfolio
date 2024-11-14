using UnityEngine;

/// <summary>
/// Manages visibility animations for the portfolio canvas by controlling the CanvasGroup properties.
/// </summary>
public class ReadablePortfolioAnimator
{
    // CanvasGroup used to adjust the canvas visibility and interactivity.
    private CanvasGroup _canvasGroup;

    /// <summary>
    /// Constructor that initializes the ReadablePortfolioAnimator with a CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup controlling visibility and interaction of the canvas.</param>
    public ReadablePortfolioAnimator(CanvasGroup canvasGroup)
    {
        _canvasGroup = canvasGroup;
    }

    /// <summary>
    /// Sets the visibility of the canvas by adjusting the alpha, interactable, and blocksRaycasts properties.
    /// </summary>
    /// <param name="isVisible">True to show the canvas; false to hide it.</param>
    public void SetVisibility(bool isVisible)
    {
        // Set alpha to 1 (visible) or 0 (invisible) based on isVisible.
        _canvasGroup.alpha = isVisible ? 1 : 0;

        // Enable or disable interaction based on visibility.
        _canvasGroup.interactable = isVisible;

        // Control whether the canvas blocks raycasts, depending on visibility.
        _canvasGroup.blocksRaycasts = isVisible;
    }
}
