using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayBeer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI beerTMP;
    private int _beerAmount;

    private IEnumerator IncreaseTextAmount() {
        _beerAmount++;
        if (_beerAmount == GameManager.GetBeerCount()) yield return null;
        beerTMP.text = _beerAmount.ToString();
        yield return new WaitForSeconds(0.01f);

    }
}