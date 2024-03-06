using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeArmeSysteme : MonoBehaviour
{
    [SerializeField] private int requiredBeerCount;
    public TMP_Text beerCountText;
    public Button button; 

    void Update()
    {
        beerCountText.text = "" + GameManager.beerCount;
        
        if (GameManager.beerCount >= requiredBeerCount)
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
        GameManager.UpgradeItem("Arme");
    }
} 