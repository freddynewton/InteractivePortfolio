using System.Linq;
using UnityEngine;

/// <summary>
/// Controls the visibility and alignment of the readable portfolio canvas, 
/// managing UI elements and their animations within the canvas.
/// </summary>
public class ReadablePortfolioCanvasController : MonoBehaviour
{
    // The CanvasGroup component controlling the canvas' transparency and interactivity.
    [SerializeField] private CanvasGroup _canvasGroup;

    // The ScrollViewController associated with managing scrolling functionality for the canvas content.
    [SerializeField] private ScrollViewController _scrollViewController;

    // Animator handling the visibility animations of the readable portfolio canvas.
    private ReadablePortfolioAnimator _readablePortfolioAnimator;

    private ChapterButton[] chapterButtons;

    // Property providing external access to the ScrollViewController instance.
    public ScrollViewController ScrollViewController
    {
        get
        {
            return _scrollViewController;
        }
    }

    /// <summary>
    /// Sets the visibility of the portfolio canvas by triggering visibility animations.
    /// </summary>
    /// <param name="isVisible">True to make the canvas visible; false to hide it.</param>
    public void SetVisibility(bool isVisible)
    {
        _readablePortfolioAnimator.SetVisibility(isVisible);
    }

    public void ResetAllFocusedChapterButton(ChapterButton exception)
    {
        chapterButtons.ToList().ForEach(chapterButton =>
        {
            if (chapterButton != exception)
            {
                chapterButton.SetBackgroundFocused(false);
            }
        });
    }

    private void Update()
    {

    }

    /// <summary>
    /// Initializes necessary components and child elements on awake.
    /// </summary>
    private void Awake()
    {
        // Instantiate the animator responsible for handling canvas visibility animations.
        _readablePortfolioAnimator = new ReadablePortfolioAnimator(_canvasGroup);

        // Initially hide the canvas upon starting.
        _readablePortfolioAnimator.SetVisibility(false);

        // Retrieve all child ChapterButton components, including inactive ones.
        chapterButtons = GetComponentsInChildren<ChapterButton>(true);

        // Initialize each ChapterButton with a reference to this controller.
        chapterButtons.ToList().ForEach(chapterButton => chapterButton.Initialize(this));
    }
}
