using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeCravateSysteme : MonoBehaviour
{
    
    public TMP_Text beerCountText;
    public Cravate cravate;
    public Button button;

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
            
            cravate.level++;
            
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
        return 300f * Mathf.Pow(1.5f, cravate.level - 1);
    }
}