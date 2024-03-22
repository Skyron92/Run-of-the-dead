using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonUpgrade : MonoBehaviour
{
    public TMP_Text beerCountText;
    public TMP_Text levelDisplay;
    public TMP_Text costDisplay;
    public Button button;
    [SerializeField] private ItemType type;
    [SerializeField] private GameObject indicator;
    
    private void Awake() {
        GameManager.BeerCountChanged += () => OnBeerCountChanged();
    }

    private void Start() {
        DisplayIndicator();
    }

    private void DisplayIndicator() {
        indicator.SetActive(GameManager.GetBeerCount() >= Item.GetCost(type));
        levelDisplay.text = GameManager.GetChaussureLevel().ToString();
        beerCountText.text = GameManager.GetBeerCount().ToString();
        costDisplay.text = Item.GetCost(type).ToString();
        Debug.Log(GameManager.GetBeerCount());
    }

    private void OnBeerCountChanged() {
        button.interactable = GameManager.GetBeerCount() >= Item.GetCost(type);
        DisplayIndicator();
    }
    
    public void OnButtonClicked()
    {
        if (GameManager.GetBeerCount() >= Item.GetCost(type))
        {
            GameManager.SetBeerCount(-Item.GetCost(type));
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

    private void OnDestroy() {
        GameManager.BeerCountChanged -= OnBeerCountChanged;
    }
}

public enum ItemType {
    Arme, Chaussure, Cravate
}