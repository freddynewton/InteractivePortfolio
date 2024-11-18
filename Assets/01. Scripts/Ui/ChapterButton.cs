using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a clickable chapter button within the readable portfolio.
/// </summary>
public class ChapterButton : AnimatedButton
{
    [Header("Chapter Button Settings")]
    [SerializeField] private RectTransform _scrollViewTarget;
    private ReadablePortfolioCanvasController _readablePortfolioCanvasController;
    private bool isInitialized;

    /// <summary>
    /// Initializes the chapter button with portfolio canvas controller reference.
    /// </summary>
    public void Initialize(ReadablePortfolioCanvasController readablePortfolioCanvasController)
    {
        if (isInitialized) return;

        _readablePortfolioCanvasController = readablePortfolioCanvasController;
        isInitialized = true;
    }

    /// <summary>
    /// Sets the button's visibility using fade animation.
    /// </summary>
    public void SetVisibility(float amount)
    {
        panelCanvasGroup.DOFade(amount, fadeDuration);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        _readablePortfolioCanvasController.ResetAllFocusedChapterButton(this);
        SetBackgroundFocused(true);
        _readablePortfolioCanvasController.ScrollViewController.SnapTo(_scrollViewTarget);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        transform.DOScale(1f, 0.1f);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        transform.DOScale(0.8f, 0.1f);
    }

    protected override void Awake()
    {
        base.Awake();
        transform.localScale = Vector3.one * 0.8f;
        panelCanvasGroup.alpha = defaultAlpha;
    }
}
