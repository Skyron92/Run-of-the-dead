using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
    }

    private void Start()
    {
        Debug.Log(Item.GetCost(type));
        beerCountText.text = GameManager.GetBeerCount().ToString();
    }

    private void DisplayIndicator() {
        indicator.SetActive(GameManager.GetBeerCount() >= Item.GetCost(type));
    }

    private void OnBeerCountChanged() {
        button.interactable = GameManager.GetBeerCount() >= Item.GetCost(type);
        beerCountText.text = GameManager.GetBeerCount().ToString();
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

    private void OnDestroy()
    {
        GameManager.BeerCountChanged -= OnBeerCountChanged;
    }
}

public enum ItemType {
    Arme, Chaussure, Cravate
}