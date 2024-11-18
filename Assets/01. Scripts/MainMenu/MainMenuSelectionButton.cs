using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// Handles hover, click, and fade effects for main menu selection buttons,
/// and allows switching to a specified scene on click.
/// </summary>
public class MainMenuSelectionButton : AnimatedButton
{
    [Header("Action Settings")]
    [SerializeField] private bool useCustomAction = false;
    [SerializeField] private UnityEvent customAction;

    [Header("Scene Settings")]
    [SerializeField] private SceneName sceneToLoad;
    [SerializeField] private bool blockOnClick = false;

    [Header("Content Settings")]
    [SerializeField] private RectTransform contentContainer;
    [SerializeField] private float hoverScale = 1.1f;

    [Header("Animation Settings")]
    [SerializeField] private ImageSpriteSheetAnimator iconAnimator;
    [SerializeField] private Sprite[] hoverSpritesSprites;
    [SerializeField] private Sprite[] loadInAnimation;

    /// <summary>
    /// Starts the fade-in animation for the button icon with a delay.
    /// </summary>
    public void StartFadeInAnimation(float delay)
    {
        StartCoroutine(StartLoadInAnimation(delay));
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        AnimateContentOnHover(true);
        StartIconHoverAnimation();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        AnimateContentOnHover(false);
        StopIconHoverAnimation();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        HandleButtonClick();
    }

    protected override void Awake()
    {
        base.Awake();
        InitializeButton();
    }

    #region Private Methods

    private void AnimateContentOnHover(bool isHovering)
    {
        float targetScale = isHovering ? hoverScale : 1f;
        contentContainer.DOScale(targetScale, fadeDuration).SetEase(Ease.OutCubic);
    }

    private void StartIconHoverAnimation()
    {
        iconAnimator?.StartAnimation(hoverSpritesSprites, true);
    }

    private void StopIconHoverAnimation()
    {
        iconAnimator?.SetLoop(false);
    }

    private void HandleButtonClick()
    {
        if (useCustomAction)
        {
            customAction?.Invoke();
            return;
        }

        if (!blockOnClick)
        {
            _ = SceneController.Instance.SwitchSceneAsync(SceneName.MainMenu, sceneToLoad);
        }
    }

    private void InitializeButton()
    {
        panelCanvasGroup.alpha = 0;
        iconAnimator?.SetFirstFrame(loadInAnimation);
    }

    private IEnumerator StartLoadInAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        iconAnimator?.PlayOneShotAnimation(loadInAnimation);
    }

    #endregion
}
