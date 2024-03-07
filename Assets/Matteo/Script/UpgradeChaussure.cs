using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeChaussureSysteme : MonoBehaviour
{
    
    public TMP_Text beerCountText;
    public ItemType itemType;
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

    // Méthode appelée lorsque le bouton est cliqué
    public void OnButtonClicked()
    {
        // Vérifie si le joueur a assez de bière pour améliorer l'item
        if (GameManager.beerCount >= GetUpgradeCost())
        {
            // Réduit le nombre de bières du coût de l'amélioration
            GameManager.beerCount -= (int)GetUpgradeCost();

            // Augmente le niveau de l'item
            switch (itemType)
            {
                case ItemType.Chaussure:
                    GameManager.chaussureLevel++;
                    break;
            }
            Debug.Log(GetUpgradeCost());

            Debug.Log("Item upgraded!");
        }
        else
        {
            Debug.Log("Not enough beers to upgrade the item.");
        }
    }

    // Méthode pour obtenir le coût d'amélioration de l'item en fonction de son niveau actuel
    float GetUpgradeCost()
    {
        switch (itemType)
        {
            case ItemType.Chaussure:
                return 300f * Mathf.Pow(1.5f, GameManager.chaussureLevel - 1);
            default: 
                return 0f;
        }
    }
}