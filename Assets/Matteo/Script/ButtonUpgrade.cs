using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonUpgrade : MonoBehaviour
{
    public TMP_Text beerCountText;
    public TMP_Text levelDisplay;
    public Button button;
    [SerializeField] private ItemType type;
    [SerializeField] private GameObject indicator;
    
    private void Awake() {
        GameManager.BeerCountChanged += () => OnBeerCountChanged();
        DisplayIndicator();
    }

    private void Start() {
        Debug.Log(Item.GetCost(type));
        beerCountText.text = GameManager.GetBeerCount().ToString();
    }

    private void DisplayIndicator() {
        indicator.SetActive(GameManager.GetBeerCount() >= Item.GetCost(type));
        levelDisplay.text = GameManager.GetChaussureLevel().ToString();
    }

    private void OnBeerCountChanged() {
        button.interactable = GameManager.GetBeerCount() >= Item.GetCost(type);
        beerCountText.text = GameManager.GetBeerCount().ToString();
        DisplayIndicator();
        Debug.Log(GameManager.GetBeerCount());
    }
    void Update() {
        beerCountText.text = GameManager.GetBeerCount().ToString();
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