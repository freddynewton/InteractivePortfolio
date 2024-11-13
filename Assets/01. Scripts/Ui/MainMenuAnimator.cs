using DG.Tweening;
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles all animations for the main menu elements, including social media buttons,
/// title, name, and menu buttons.
/// </summary>
public class MainMenuAnimator
{
    private RectTransform socialMediaButtonsContainer;
    private CanvasGroup titleCanvasGroup;
    private CanvasGroup nameCanvasGroup;
    private CanvasGroup mainMenuCanvasGroup;
    private RectTransform menuButtonsContainer;
    private CanvasGroup startText;
    private Tween startTextSequence;

    /// <summary>
    /// Initializes the animator with the required UI elements.
    /// </summary>
    public MainMenuAnimator(
        RectTransform socialMediaButtons,
        CanvasGroup title,
        CanvasGroup name,
        CanvasGroup mainMenu,
        RectTransform menuButtons,
        CanvasGroup start)
    {
        socialMediaButtonsContainer = socialMediaButtons;
        titleCanvasGroup = title;
        nameCanvasGroup = name;
        mainMenuCanvasGroup = mainMenu;
        menuButtonsContainer = menuButtons;
        startText = start;
    }

    /// <summary>
    /// Controls the visibility of the main menu with a fade effect.
    /// </summary>
    public void SetMenuVisibility(bool active)
    {
        mainMenuCanvasGroup.DOFade(active ? 1 : 0, 0.3f).SetEase(Ease.OutCubic);
        mainMenuCanvasGroup.interactable = active;
        mainMenuCanvasGroup.blocksRaycasts = active;
    }

    /// <summary>
    /// Instantly hides all main menu elements, setting their initial states for animations.
    /// </summary>
    public void HideMainMenuInstant()
    {
        socialMediaButtonsContainer.gameObject.SetActive(false);
        titleCanvasGroup.alpha = 0;
        nameCanvasGroup.alpha = 0;
        mainMenuCanvasGroup.alpha = 0;
        menuButtonsContainer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows the social media buttons with a sliding animation.
    /// </summary>
    public void ShowSocialMediaButtons(float startY, float endY, float fadeDuration)
    {
        socialMediaButtonsContainer.anchoredPosition = new Vector2(socialMediaButtonsContainer.anchoredPosition.x, startY);
        socialMediaButtonsContainer.gameObject.SetActive(true);
        socialMediaButtonsContainer.DOAnchorPosY(endY, fadeDuration).SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Fades in the title with a smooth animation.
    /// </summary>
    public void ShowTitle(float fadeDuration)
    {
        titleCanvasGroup.alpha = 0;
        titleCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Fades in the name text with a specified delay.
    /// </summary>
    public void ShowName(float fadeDuration, float delay)
    {
        nameCanvasGroup.alpha = 0;
        nameCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.OutCubic).SetDelay(delay);
    }

    /// <summary>
    /// Sequentially shows the menu buttons with scaling animations.
    /// </summary>
    public void ShowMenuButtons(float delayIncrement)
    {
        RectTransform[] buttons = menuButtonsContainer.GetComponentsInChildren<RectTransform>();

        foreach (RectTransform button in buttons)
        {
            button.localScale = Vector3.zero;
            button.gameObject.SetActive(true);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform button = buttons[i];
            button.DOScale(1, 1).SetEase(Ease.OutBack).SetDelay(i * delayIncrement);
            button.GetComponent<MainMenuSelectionButton>()?.StartFadeInAnimation(i * 0.15f);
        }
    }

    /// <summary>
    /// Starts the looping fade and scale effect for the start text.
    /// </summary>
    public void StartFadeEffectStartText()
    {
        startText.alpha = 1;
        startText.transform.localScale = Vector3.one;

        startTextSequence = DOTween.Sequence()
            .Append(startText.DOFade(0.7f, 2).SetEase(Ease.OutCubic))
            .Join(startText.transform.DOScale(1.05f, 2).SetEase(Ease.InOutCubic))
            .SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// Hides the start text with a quick fade-out effect.
    /// </summary>
    public void HideStartText()
    {
        startTextSequence.Kill();
        startText.DOFade(0, 0.1f).SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Shows the main menu in a sequential animation. This coroutine handles the order
    /// and timing of showing each element in the main menu.
    /// </summary>
    public IEnumerator ShowMainMenu(float socialMediaButtonsStartY, float socialMediaButtonsEndY, float socialMediaButtonsFadeDuration, float fadeTextDuration)
    {
        HideStartText();
        SetMenuVisibility(true);

        // Delay before showing social media buttons
        yield return new WaitForSecondsRealtime(0.5f);

        ShowSocialMediaButtons(socialMediaButtonsStartY, socialMediaButtonsEndY, socialMediaButtonsFadeDuration);
        yield return new WaitForSecondsRealtime(socialMediaButtonsFadeDuration * 2);

        ShowTitle(fadeTextDuration);
        ShowName(fadeTextDuration, fadeTextDuration);
        yield return new WaitForSecondsRealtime(fadeTextDuration * 2);

        ShowMenuButtons(0.1f);
    }
}
