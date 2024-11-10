using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

/// <summary>
/// Handles hover, click, and fade effects for main menu selection buttons,
/// and allows switching to a specified scene on click.
/// </summary>
public class MainMenuSelectionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Panel Fade Settings")]
    [Tooltip("The CanvasGroup of the panel to fade in and out.")]
    [SerializeField] private CanvasGroup panelCanvasGroup;

    [Tooltip("Duration of the fade animation in seconds.")]
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Scene Settings")]
    [Tooltip("The scene to load when the button is clicked.")]
    [SerializeField] private SceneName sceneToLoad;

    [Tooltip("Whether to block further actions after the button is clicked.")]
    [SerializeField] private bool blockOnClick = false;

    [Header("Content Container Settings")]
    [Tooltip("The RectTransform of the content container.")]
    [SerializeField] private RectTransform contentContainer;

    [Tooltip("The scale to apply when hovering over the button.")]
    [SerializeField] private float hoverScale = 1.1f;

    [Header("Icon Settings")]
    [SerializeField] private ImageSpriteSheetAnimator iconAnimator;

    [SerializeField] private Sprite[] hoverSpritesSprites;
    [SerializeField] private Sprite[] loadInAnimation;

    /// <summary>
    /// Starts the fade-in animation for the button icon.
    /// </summary>
    public void StartFadeInAnimation(float delay)
    {
        StartCoroutine(StartLoadInAnimation(delay));
    }

    /// <summary>
    /// Triggered when the pointer enters the button area, fades in the panel.
    /// </summary>
    /// <param name="eventData">Event data associated with the pointer entering.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound
        SoundManager.Instance.PlaySound(SoundType.UiWhoosh);

        // Scale up the content container
        contentContainer.DOScale(hoverScale, fadeDuration).SetEase(Ease.OutCubic);

        // Fade in the panel
        panelCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.OutCubic);

        // Start the icon hover animation
        iconAnimator?.StartAnimation(hoverSpritesSprites, true);
    }

    /// <summary>
    /// Triggered when the pointer exits the button area, fades out the panel.
    /// </summary>
    /// <param name="eventData">Event data associated with the pointer exiting.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Scale down the content container
        contentContainer.DOScale(1, fadeDuration).SetEase(Ease.OutCubic);

        // Fade out the panel
        panelCanvasGroup.DOFade(0, fadeDuration).SetEase(Ease.OutCubic);

        // Stop the icon hover animation
        iconAnimator?.SetLoop(false);
    }

    /// <summary>
    /// Triggered when the button is clicked, loads the specified scene.
    /// </summary>
    /// <param name="eventData">Event data associated with the pointer click.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Play click sound
        SoundManager.Instance.PlaySound(SoundType.UiClick);

        // If blockOnClick is true, exit without loading the scene
        if (blockOnClick)
        {
            return;
        }

        // Switch to the target scene
        _ = SceneController.Instance.SwitchSceneAsync(SceneName.MainMenu, sceneToLoad);
    }

    /// <summary>
    /// Initializes the CanvasGroup and ensures it starts hidden.
    /// </summary>
    private void Awake()
    {
        // Set default CanvasGroup and ensure it's hidden initially
        panelCanvasGroup ??= GetComponent<CanvasGroup>();
        panelCanvasGroup.alpha = 0;
        iconAnimator.SetFirstFrame(loadInAnimation);
    }

    private IEnumerator StartLoadInAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);

        iconAnimator.PlayOneShotAnimation(loadInAnimation);
    }
}
