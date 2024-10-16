using System;
using UnityEngine;

public class WorldButton : MonoBehaviour, IInteractable
{
    public ButtonType buttonType;

    [SerializeField] private PortfolioData portfolioData;

    private void Awake()
    {
        
    }

    public void Interact()
    {
        if (buttonType == ButtonType.PortfolioData)
        {
            PopupController.Instance.Show(portfolioData);
        }

        Debug.Log("Interacted with WorldButton");
    }
}

public enum ButtonType
{
    PortfolioData,
}
