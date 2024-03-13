using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeChaussureSysteme : MonoBehaviour
{
    public static UpgradeChaussureSysteme Current;
    public TMP_Text beerCountText;
    public Button button;

    private Item _item = new Item("Chaussure", 1, 15f, 200f);

    private void Awake()
    {
        Current = this;
    }

    void Update()
    {
        beerCountText.text = "" + GameManager.GetBeerCount();
        
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
            
            //Debug.Log("Stats for Chaussure: " + GameManager.GetChaussureStat());
            
            Debug.Log("Item upgraded!");
        }
        else
        {
            Debug.Log("Not enough beers to upgrade the item.");
        }
    }
    float GetUpgradeCost()
    {
        return 200f * Mathf.Pow(1.5f, _item.level);
    }
    
    public Item GetItem()
    {
        return _item;
    }
}