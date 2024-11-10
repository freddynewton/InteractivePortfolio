using DG.Tweening;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the main menu UI canvas, controlling the appearance of social media buttons,
/// title, name, and menu buttons with fade and scale animations.
/// </summary>
public class MainMenuCanvasController : Singleton<MainMenuCanvasController>
{
    [Header("Social Media Buttons")]
    [Tooltip("Container for social media buttons.")]
    [SerializeField] private RectTransform socialMediaButtonsContainer;

    [Tooltip("Starting Y offset position for the social media buttons.")]
    [SerializeField] private float socialMediaButtonsStartY = 50f;

    [Tooltip("End Y offset position for the social media buttons after animation.")]
    [SerializeField] private float socialMediaButtonsEndY = -25f;

    [Tooltip("Duration of the social media buttons fade-in animation.")]
    [SerializeField] private float socialMediaButtonsFadeDuration = 0.2f;

    [Header("Title and Name Fade Settings")]
    [Tooltip("CanvasGroup for the title, allowing fade animations.")]
    [SerializeField] private CanvasGroup titleCanvasGroup;

    [Tooltip("CanvasGroup for the name, allowing fade animations.")]
    [SerializeField] private CanvasGroup nameCanvasGroup;

    [Tooltip("Duration for fading in the title and name text.")]
    [SerializeField] private float fadeTextDuration = 0.2f;

    [Header("Menu Buttons")]
    [Tooltip("Container for menu buttons.")]
    [SerializeField] private RectTransform menuButtonsContainer;

    [Header("Start Text")]
    [Tooltip("The Canvas of the start text.")]
    [SerializeField] private CanvasGroup startText;

    private Tween startTextSequence;  // Tween for the start text fade effect
    private bool isMainMenuShown = false;  // Tracks if the main menu is currently displayed

    /// <summary>
    /// Initializes the canvas by hiding all menu elements and starting the start text animation.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        HideMainMenuInstant();
        StartFadeEffectStartText();
    }

    /// <summary>
    /// Coroutine to show the main menu with a sequential animation of elements.
    /// </summary>
    public IEnumerator ShowMainMenu()
    {
        HideStartText();

        yield return new WaitForSecondsRealtime(0.5f); // Delay before showing the main menu

        ShowSocialMediaButtons();
        yield return new WaitForSecondsRealtime(socialMediaButtonsFadeDuration * 2);

        ShowTitle();
        ShowName(fadeTextDuration);
        yield return new WaitForSecondsRealtime(fadeTextDuration * 2);

        ShowMenuButtons();
    }

    /// <summary>
    /// Checks for any input (key or touch) to trigger the main menu display.
    /// </summary>
    private void Update()
    {
        if (!isMainMenuShown)
        {
            CheckForAnyInput();
        }
    }

    /// <summary>
    /// Checks for user input to trigger the main menu animation.
    /// </summary>
    private void CheckForAnyInput()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            if (!isMainMenuShown)
            {
                StartCoroutine(ShowMainMenu());
                isMainMenuShown = true;
            }
        }
    }

    /// <summary>
    /// Hides all main menu elements instantly, setting their initial states for animation.
    /// </summary>
    private void HideMainMenuInstant()
    {
        socialMediaButtonsContainer.gameObject.SetActive(false);
        titleCanvasGroup.alpha = 0;
        nameCanvasGroup.alpha = 0;
        menuButtonsContainer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows the social media buttons with a sliding animation.
    /// </summary>
    private void ShowSocialMediaButtons()
    {
        socialMediaButtonsContainer.anchoredPosition = new Vector2(socialMediaButtonsContainer.anchoredPosition.x, socialMediaButtonsStartY);
        socialMediaButtonsContainer.gameObject.SetActive(true);
        socialMediaButtonsContainer.DOAnchorPosY(socialMediaButtonsEndY, socialMediaButtonsFadeDuration).SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Fades in the title with a smooth fade animation.
    /// </summary>
    private void ShowTitle()
    {
        titleCanvasGroup.alpha = 0;
        titleCanvasGroup.DOFade(1, fadeTextDuration).SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Fades in the name text with a delay and a smooth fade animation.
    /// </summary>
    /// <param name="delay">The delay before the name text fades in.</param>
    private void ShowName(float delay)
    {
        nameCanvasGroup.alpha = 0;
        nameCanvasGroup.DOFade(1, fadeTextDuration).SetEase(Ease.OutCubic).SetDelay(delay);
    }

    /// <summary>
    /// Shows the menu buttons sequentially with scaling animations.
    /// </summary>
    private void ShowMenuButtons()
    {
        RectTransform[] buttons = menuButtonsContainer.GetComponentsInChildren<RectTransform>();

        // Set all buttons to scale 0 initially
        foreach (RectTransform button in buttons)
        {
            button.localScale = Vector3.zero;
            button.gameObject.SetActive(true);
        }

        // Animate the buttons with a delay
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform button = buttons[i];
            button.localScale = Vector3.zero;
            button.DOScale(1, 1).SetEase(Ease.OutBack).SetDelay(i * 0.1f);
            button.GetComponent<MainMenuSelectionButton>()?.StartFadeInAnimation(i * 0.15f);
        }
    }

    /// <summary>
    /// Starts the fade and scale effect for the start text with looping animation.
    /// </summary>
    private void StartFadeEffectStartText()
    {
        startText.alpha = 1;
        startText.transform.localScale = Vector3.one; // Reset scale to default

        // Create a sequence for fade and scale animations
        startTextSequence = DOTween.Sequence()
            .Append(startText.DOFade(0.7f, 2).SetEase(Ease.OutCubic))
            .Join(startText.transform.DOScale(1.05f, 2).SetEase(Ease.InOutCubic))
            .SetLoops(-1, LoopType.Yoyo); // Loop indefinitely with Yoyo effect
    }

    /// <summary>
    /// Hides the start text with a fade-out effect.
    /// </summary>
    private void HideStartText()
    {
        startTextSequence.Kill();
        startText.DOFade(0, 0.1f).SetEase(Ease.OutCubic);
    }
}
