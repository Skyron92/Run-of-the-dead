using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeChaussureSysteme : MonoBehaviour
{
    [SerializeField] private int beerCount; 
    [SerializeField] private int requiredBeerCount;
    
    
    public Button button;
    public TMP_Text beerCountText;

    void Update()
    {
        beerCountText.text = "" + beerCount;
        
        if (beerCount >= requiredBeerCount)
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
        if (beerCount >= requiredBeerCount)
        {
            beerCount -= requiredBeerCount;
            
            requiredBeerCount *= 2;
            
            Debug.Log("Beer count: " + beerCount);
            Debug.Log("Next required beer count: " + requiredBeerCount);
            
            UpdateButtonInteractivity();
        }
        else
        {
            Debug.Log("Not enough beers to interact with the button.");
        }
    }
    
    private void UpdateButtonInteractivity()
    {
        if (beerCount >= requiredBeerCount)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    } 
}
