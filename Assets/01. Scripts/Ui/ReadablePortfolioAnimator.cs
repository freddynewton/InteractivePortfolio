using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadablePortfolioAnimator
{
    private CanvasGroup _canvasGroup;

    public ReadablePortfolioAnimator(CanvasGroup canvasGroup)
    {
        _canvasGroup = canvasGroup;
    }

    public void SetVisibility(bool isVisible)
    {
        _canvasGroup.alpha = isVisible ? 1 : 0;
        _canvasGroup.interactable = isVisible;
        _canvasGroup.blocksRaycasts = isVisible;
    }
}
