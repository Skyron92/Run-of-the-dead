using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonUpgrade : MonoBehaviour
{
    public static ButtonUpgrade Current;
    public TMP_Text beerCountText;
    public Button button;
    [SerializeField] private ItemType type;
    
    private void Awake()
    {
        Current = this;
        GameManager.BeerCountChanged += () => OnBeerCountChanged();
    }

    private void OnBeerCountChanged()
    {
        if (GameManager.GetBeerCount() >= Item.GetCost(type)) button.interactable = true;
        else button.interactable = false;
    }
    void Update()
    {
        beerCountText.text = "" + GameManager.GetBeerCount();
    }
    
    public void OnButtonClicked()
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
}

public enum ItemType {
    Arme, Chaussure, Cravate
}