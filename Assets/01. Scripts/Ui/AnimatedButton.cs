using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Panel Fade Settings")]
    [Tooltip("The CanvasGroup of the panel to fade in and out.")]
    [SerializeField] protected CanvasGroup panelCanvasGroup;

    [Tooltip("Duration of the fade animation in seconds.")]
    [SerializeField] protected float fadeDuration = 0.3f;

    [Tooltip("Default alpha value for the panel.")]
    [SerializeField] protected float defaultAlpha = 0f;

    [SerializeField] protected bool isBackgroundFocused = false;

    [Header("Sound Settings")]
    [SerializeField] protected SoundType clickSound = SoundType.UiClick;
    [SerializeField] protected SoundType hoverSound = SoundType.UiWhoosh;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // Play click sound
        SoundManager.Instance.PlaySound(clickSound);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound
        SoundManager.Instance.PlaySound(hoverSound);

        if (isBackgroundFocused)
        {
            return;
        }

        // Fade in the panel
        panelCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.OutCubic);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (isBackgroundFocused)
        {
            return;
        }

        // Fade in the panel
        panelCanvasGroup.DOFade(defaultAlpha, fadeDuration).SetEase(Ease.OutCubic);
    }

    public void SetBackgroundFocused(bool isFocused)
    {
        isBackgroundFocused = isFocused;

        if (isFocused)
        {
            panelCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.OutCubic);
        }
        else
        {
            panelCanvasGroup.DOFade(defaultAlpha, fadeDuration).SetEase(Ease.OutCubic);
        }
    }

    protected virtual void Awake()
    {
        panelCanvasGroup ??= GetComponentInChildren<CanvasGroup>();

        // Set the default alpha value
        panelCanvasGroup.alpha = defaultAlpha;
    }
}
