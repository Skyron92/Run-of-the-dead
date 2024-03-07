using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeArmeSysteme : MonoBehaviour
{
    
    public TMP_Text beerCountText;
    public Arme arme;
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
            
            arme.level++;
            
            Debug.Log("Stats for Arme: " + GameManager.GetArmeStat());
            
            Debug.Log("Item upgraded!");
        }
        else
        {
            Debug.Log("Not enough beers to upgrade the item.");
        }
    }
    float GetUpgradeCost()
    {
        return 300f * Mathf.Pow(1.5f, arme.level - 1);
    }
}