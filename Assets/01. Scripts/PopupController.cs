using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : Singleton<PopupController>
{
    [Header("References")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Button _websiteButton;

    private PortfolioData _portfolioData;

    private void Awake()
    {
        _canvasGroup ??= GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    public void Show(PortfolioData data)
    {
        _portfolioData = data;
        _title.text = _portfolioData.Title;
        _description.text = _portfolioData.Description;
        _image.sprite = _portfolioData.Image;
        _image.preserveAspect = true;

        _websiteButton.onClick.RemoveAllListeners();
        _websiteButton.onClick.AddListener(() =>
        {
            Application.OpenURL(_portfolioData.Website);
        });

        // Use DoTween to animate the popup
        LeanTween.alphaCanvas(_canvasGroup, 1, 0.5f).setEase(LeanTweenType.easeOutCubic);
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        // Use DoTween to animate the popup
        LeanTween.alphaCanvas(_canvasGroup, 0, 0.5f).setEase(LeanTweenType.easeOutCubic);
        _canvasGroup.blocksRaycasts = false;
    }
}
