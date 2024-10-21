using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PortfolioData", menuName = "Portfolio Data", order = 1)]
public class PortfolioData : ScriptableObject
{
    public Sprite Image;
    public string Title;
    public string Description;
    public string Website;
}
