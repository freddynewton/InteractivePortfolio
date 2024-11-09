using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// Handles the hover, click, and animation effects for a social media button,
/// allowing URL navigation and interactive visual feedback.
/// </summary>
public class SocialMediaButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("URL Configuration")]
    [Tooltip("The URL that opens when the button is clicked.")]
    [SerializeField] private string url;

    [Header("UI Elements")]
    [Tooltip("The image component of the button, used for hover and click effects.")]
    [SerializeField] private Image image;

    [Header("Hover Effects")]
    [Tooltip("The color to apply when hovering over the button.")]
    [SerializeField] private Color hoverColor = Color.gray;

    [Tooltip("The scale to apply when hovering over the button.")]
    [SerializeField] private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);

    [Tooltip("Duration of the hover animation.")]
    [SerializeField] private float animationDuration = 0.2f;

    [Tooltip("Rotation angle applied when hovering over the button.")]
    [SerializeField] private float hoverRotationAngle = 10f;

    private Tween hoverTween;       // Holds the animation tween for hover effect
    private Color originalColor;    // Stores the original color of the button image
    private Vector3 originalScale;  // Stores the original scale of the button image

    /// <summary>
    /// Initializes the original color and scale, and ensures image is assigned.
    /// </summary>
    private void Start()
    {
        image ??= GetComponent<Image>();
        originalColor = image.color;
        originalScale = image.transform.localScale;
    }

    /// <summary>
    /// Triggered when the pointer enters the button area, starts hover effects.
    /// </summary>
    /// <param name="eventData">Event data associated with the pointer entering.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound
        SoundManager.Instance.PlaySound(SoundType.UiWhoosh);

        // Set initial rotation and start a YoYo rotation tween
        image.transform.eulerAngles = new Vector3(0, 0, -hoverRotationAngle);
        hoverTween = image.transform.DORotate(new Vector3(0, 0, hoverRotationAngle), animationDuration)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        // Animate color and scale change on hover
        image.DOColor(hoverColor, animationDuration);
        image.transform.DOScale(hoverScale, animationDuration);
    }

    /// <summary>
    /// Triggered when the pointer exits the button area, stops hover effects.
    /// </summary>
    /// <param name="eventData">Event data associated with the pointer exiting.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Stop the hover rotation tween
        hoverTween.Kill();

        // Rotate back to original position smoothly
        image.transform.DORotate(Vector3.zero, animationDuration / 2).SetEase(Ease.InOutSine);

        // Revert to the original color and scale
        image.DOColor(originalColor, animationDuration);
        image.transform.DOScale(originalScale, animationDuration);
    }

    /// <summary>
    /// Triggered when the button is clicked, opens the URL in a web browser.
    /// </summary>
    /// <param name="eventData">Event data associated with the pointer click.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Play click sound
        SoundManager.Instance.PlaySound(SoundType.UiClick);

        // Open the URL in the default web browser
        Application.OpenURL(url);
    }
}
