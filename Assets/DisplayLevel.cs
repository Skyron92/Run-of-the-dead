using TMPro;
using UnityEngine;

public class DisplayLevel : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Awake() {
        tmp = GetComponent<TextMeshProUGUI>();
        GameManager.BeerCountChanged += OnBeerCountChanged;
    }

    private void OnBeerCountChanged()
    {
        tmp.text = GameManager.GetChaussureLevel().ToString();
    }
}
