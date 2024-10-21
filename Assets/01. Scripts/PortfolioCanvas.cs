using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioCanvas : MonoBehaviour
{
    [SerializeField] private PortfolioData _portfolioData;

    [Header("References")]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _title;

    private void Awake()
    {
        _image.sprite = _portfolioData.Image;
        _image.preserveAspect = true;
        _title.text = _portfolioData.Title;
    }
}
