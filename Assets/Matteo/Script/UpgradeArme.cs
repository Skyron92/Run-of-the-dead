using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeArmeSysteme : MonoBehaviour
{
    public static UpgradeArmeSysteme Current;
    public TMP_Text beerCountText;
    public Button button;

    private Item _item = new Item("Arme",1, 20f, 300f);

    private void Awake()
    {
        Current = this;
    }

    void Update()
    {
        beerCountText.text = "" + GameManager.GetBeerCount();
        
        // Vérifie si le joueur a assez de bière pour améliorer l'item
        if (GameManager.GetBeerCount() >= GetUpgradeCost())
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void OnButtonClicked()
    {
        if (GameManager.GetBeerCount() >= GetUpgradeCost())
        {
            GameManager.SetBeerCount(-(int)GetUpgradeCost());
            _item.level++;
            
//            Debug.Log("Stats for Arme: " + GameManager.GetArmeStat());
            
            Debug.Log("Item upgraded!");
        }
        else
        {
            Debug.Log("Not enough beers to upgrade the item.");
        }
    }
    float GetUpgradeCost()
    {
        return _item.upgradeCost * Mathf.Pow(1.5f, _item.level);
        
    }

    public Item GetItem()
    {
        return _item;
    }
}