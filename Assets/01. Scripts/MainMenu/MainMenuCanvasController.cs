using UnityEngine;

public class MainMenuCanvasController : Singleton<MainMenuCanvasController>
{
    [Header("Social Media Buttons")]
    [SerializeField] private RectTransform socialMediaButtonsContainer;
    [SerializeField] private float socialMediaButtonsStartY = 50f;
    [SerializeField] private float socialMediaButtonsEndY = -25f;
    [SerializeField] private float socialMediaButtonsFadeDuration = 0.2f;

    [Header("Title and Name Fade Settings")]
    [SerializeField] private CanvasGroup titleCanvasGroup;
    [SerializeField] private CanvasGroup nameCanvasGroup;
    [SerializeField] private float fadeTextDuration = 0.2f;

    [Header("Components")]
    [SerializeField] private RectTransform menuButtonsContainer;
    [SerializeField] private CanvasGroup startText;
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;

    private MainMenuAnimator animator;
    private bool isMainMenuShown = false;

    public override void Awake()
    {
        base.Awake();
        animator = new MainMenuAnimator(socialMediaButtonsContainer, titleCanvasGroup, nameCanvasGroup, mainMenuCanvasGroup, menuButtonsContainer, startText);
        animator.HideMainMenuInstant();
        animator.StartFadeEffectStartText();
    }

    /// <summary>
    /// Starts the process of showing the main menu, utilizing the animator.
    /// </summary>
    public void SetMenuVisibility(bool active)
    {
        animator.SetMenuVisibility(active);
    }

    /// <summary>
    /// Triggers the main menu animations in sequence.
    /// </summary>
    public void StartMainMenu()
    {
        StartCoroutine(animator.ShowMainMenu(socialMediaButtonsStartY, socialMediaButtonsEndY, socialMediaButtonsFadeDuration, fadeTextDuration));
        isMainMenuShown = true;
    }

    private void Update()
    {
        if (!isMainMenuShown)
        {
            CheckForAnyInput();
        }
    }

    private void CheckForAnyInput()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            if (!isMainMenuShown)
            {
                StartMainMenu();
            }
        }
    }
}
