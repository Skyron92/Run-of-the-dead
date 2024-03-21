using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonUpgrade : MonoBehaviour
{
    public static ButtonUpgrade Current;
    public TMP_Text beerCountText;
    public Button button;
    [SerializeField] private ItemType type;
    [SerializeField] private GameObject indicator;
    
    private void Awake() {
        Current = this;
        GameManager.BeerCountChanged += () => OnBeerCountChanged();
        DisplayIndicator();
        beerCountText.text = GameManager.GetBeerCount().ToString();
        Debug.Log(GameManager.GetBeerCount());
    }

    private void DisplayIndicator() {
        indicator.SetActive(GameManager.GetBeerCount() >= Item.GetCost(type));
    }

    private void OnBeerCountChanged() {
        button.interactable = GameManager.GetBeerCount() >= Item.GetCost(type);
    }
    void Update() {
        beerCountText.text = GameManager.GetBeerCount().ToString();
    }
    
    public void OnButtonClicked()
    {
        if (GameManager.GetBeerCount() >= Item.GetCost(type))
        {
            GameManager.SetBeerCount(-(int)Item.GetCost(type));
            switch (type)
            {
                case ItemType.Arme:
                    GameManager.SetArmelevel(1);
                    break;
                case ItemType.Chaussure:
                    GameManager.SetChaussureLevel(1);
                    break;
                case ItemType.Cravate:
                    GameManager.SetCravateLevel(1);
                    break;
            }
        }
        DisplayIndicator();
    }
}

public enum ItemType {
    Arme, Chaussure, Cravate
}