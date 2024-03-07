using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeChaussureSysteme : MonoBehaviour
{
    
    public TMP_Text beerCountText;
    public Chaussure chaussure;
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
            
            chaussure.level++;
            
            Debug.Log("Stats for Chaussure: " + GameManager.GetChaussureStat());
            
            Debug.Log("Item upgraded!");
        }
        else
        {
            Debug.Log("Not enough beers to upgrade the item.");
        }
    }
    float GetUpgradeCost()
    {
        return 300f * Mathf.Pow(1.5f, chaussure.level - 1);
    }
}