using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadablePortfolioCanvasController : MonoBehaviour
{
    private ReadablePortfolioAnimator _readablePortfolioAnimator;

    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _readablePortfolioAnimator = new ReadablePortfolioAnimator(_canvasGroup);
        _readablePortfolioAnimator.SetVisibility(false);
    }

    public void SetVisibility(bool isVisible)
    {
        _readablePortfolioAnimator.SetVisibility(isVisible);
    }
}
