using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SocialMediaButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private string url;
    [SerializeField] private Image image;
    [SerializeField] private Color hoverColor = Color.gray;          // Color on hover
    [SerializeField] private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // Scale on hover
    [SerializeField] private float animationDuration = 0.2f;         // Duration of the animation

    private Color originalColor;
    private Vector3 originalScale;

    private void Start()
    {
        image ??= GetComponent<Image>();
        originalColor = image.color;
        originalScale = image.transform.localScale;
    }

    // Triggered when the pointer enters the image area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound
        SoundManager.Instance.PlaySound(SoundType.UiWhoosh);

        // Animate color and scale with DoTween
        image.DOColor(hoverColor, animationDuration);
        image.transform.DOScale(hoverScale, animationDuration);
    }

    // Triggered when the pointer exits the image area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Animate back to original color and scale
        image.DOColor(originalColor, animationDuration);
        image.transform.DOScale(originalScale, animationDuration);
    }

    // Triggered when the image is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Play click sound
        SoundManager.Instance.PlaySound(SoundType.UiClick);

        // Open the URL
        Application.OpenURL(url);
    }
}
