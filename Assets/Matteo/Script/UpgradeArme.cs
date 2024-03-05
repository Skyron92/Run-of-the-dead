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
        GetComponent<Button>().image.color = Color.red;
        if (GameManager.beerCount >= requiredBeerCount)
        {
            GameManager.beerCount -= requiredBeerCount;
            
            requiredBeerCount *= 2;
            
            Debug.Log("Beer count: " + GameManager.beerCount);
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
        if (GameManager.beerCount >= requiredBeerCount)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    } 
}