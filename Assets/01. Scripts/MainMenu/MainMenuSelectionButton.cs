using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainMenuSelectionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CanvasGroup panelCanvasGroup;  // CanvasGroup of the panel to fade
    [SerializeField] private float fadeDuration = 0.5f;     // Duration of the fade animation
    [SerializeField] private SceneName sceneToLoad;         // Name of the scene to load on click
    [SerializeField] private bool blockOnClick = false;

    private void Awake()
    {
        // Ensure the CanvasGroup starts hidden
        panelCanvasGroup ??= GetComponent<CanvasGroup>();
        panelCanvasGroup.alpha = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound
        SoundManager.Instance.PlaySound(SoundType.UiWhoosh);

        // Fade in the panel on hover
        panelCanvasGroup.DOFade(1, fadeDuration)
            .SetEase(Ease.OutCubic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Fade out the panel when the hover ends
        panelCanvasGroup.DOFade(0, fadeDuration)
            .SetEase(Ease.OutCubic);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play click sound
        SoundManager.Instance.PlaySound(SoundType.UiClick);

        if (blockOnClick)
        {
            return;
        }

        // Switch to the specified scene
        _ = SceneController.Instance.SwitchSceneAsync(SceneName.MainMenu, sceneToLoad);
    }
}
