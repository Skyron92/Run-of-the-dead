using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeerUpdater : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _textMeshProUGUI.text = GameManager.GetBeerCount().ToString();
    }
    
}
