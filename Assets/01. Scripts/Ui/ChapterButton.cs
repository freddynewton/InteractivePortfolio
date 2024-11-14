using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a clickable chapter button within the readable portfolio, handling interactions such as clicks, hover effects, and animations.
/// </summary>
public class ChapterButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Target RectTransform for scrolling when the button is clicked.
    public RectTransform _scrollViewTarget;

    // CanvasGroup controlling visibility and interactivity of the button.
    [SerializeField] private CanvasGroup _canvasGroup;

    // Reference to the main portfolio canvas controller.
    private ReadablePortfolioCanvasController _readablePortfolioCanvasController;

    // Tracks if the button is currently focused, affecting its visibility and scaling behavior.
    public bool IsFocused { get; set; }

    // Indicates whether the button has already been initialized to avoid redundant setups.
    private bool isInitialized;

    /// <summary>
    /// Initializes the chapter button with a reference to the portfolio canvas controller.
    /// </summary>
    /// <param name="readablePortfolioCanvasController">Controller managing the overall portfolio canvas.</param>
    public void Initialize(ReadablePortfolioCanvasController readablePortfolioCanvasController)
    {
        // Prevent re-initialization if already set up.
        if (isInitialized)
        {
            return;
        }

        _readablePortfolioCanvasController = readablePortfolioCanvasController;
        isInitialized = true;
    }

    /// <summary>
    /// Sets the focus state of the button, adjusting visibility and scaling accordingly.
    /// </summary>
    /// <param name="isFocused"></param>
    public void SetFocus(bool isFocused)
    {
        // Update the focused state of the button.
        IsFocused = isFocused;

        // Adjust visibility based on the focus state.
        SetVisibility(isFocused ? 1 : 0);
    }

    /// <summary>
    /// Sets the button's visibility using a fade animation.
    /// </summary>
    /// <param name="amount">Target alpha value for visibility (0 is hidden, 1 is fully visible).</param>
    public void SetVisibility(float amount)
    {
        // Animate the alpha of the canvas group over 0.5 seconds.
        _canvasGroup.DOFade(amount, 0.5f);
    }

    /// <summary>
    /// Handles click interactions on the button.
    /// </summary>
    /// <param name="eventData">Data related to the pointer click event.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        _readablePortfolioCanvasController.ResetAllFocusedChapterButton(this);

        // Set the button as focused when clicked.
        IsFocused = true;

        // Play a click sound effect.
        SoundManager.Instance.PlaySound(SoundType.UiClick);

        // Scroll the portfolio to the target chapter associated with this button.
        _readablePortfolioCanvasController.ScrollViewController.ScrollToChild(_scrollViewTarget);
    }

    /// <summary>
    /// Handles pointer enter events for hover effects.
    /// </summary>
    /// <param name="eventData">Data related to the pointer enter event.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // If the button is not focused, increase its visibility.
        if (!IsFocused)
        {
            SetVisibility(0.3f);
        }

        // Animate the button to scale up on hover.
        transform.DOScale(1f, 0.1f);
    }

    /// <summary>
    /// Handles pointer exit events to reset hover effects.
    /// </summary>
    /// <param name="eventData">Data related to the pointer exit event.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // If the button is not focused, hide it by reducing its visibility.
        if (!IsFocused)
        {
            SetVisibility(0);
        }

        // Revert the button scale to its original size when the pointer exits.
        transform.DOScale(0.8f, 0.1f);
    }

    /// <summary>
    /// Sets up initial properties such as scale and alpha when the button is instantiated.
    /// </summary>
    private void Awake()
    {
        // Set initial scale and alpha for the button.
        transform.localScale = Vector3.one * 0.8f;
        _canvasGroup.alpha = 0;
    }
}
