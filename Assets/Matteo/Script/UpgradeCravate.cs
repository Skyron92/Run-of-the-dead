using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeCravateSysteme : MonoBehaviour
{
    public static UpgradeCravateSysteme Current;
    public TMP_Text beerCountText;
    public Button button;

    private Item _item = new Item("Cravate", 1,10f, 100f);
    
    private void Awake()
    {
        Current = this;
    }
    void Update()
    {
        beerCountText.text = "" + GameManager.beerCount;
        
        // Vérifie si le joueur a assez de bière pour améliorer l'item
        if (GameManager.beerCount >= GetUpgradeCost())
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
        if (GameManager.beerCount >= GetUpgradeCost())
        {
            GameManager.beerCount -= (int)GetUpgradeCost();
            
            _item.level++;
            
            Debug.Log("Stats for Cravate: " + GameManager.GetCravateStat());
            
            Debug.Log("Item upgraded!");
        }
        else
        {
            Debug.Log("Not enough beers to upgrade the item.");
        }
    }
    float GetUpgradeCost()
    {
        return 100f * Mathf.Pow(1.5f, _item.level);
    }
    
    public Item GetItem()
    {
        return _item;
    }
}