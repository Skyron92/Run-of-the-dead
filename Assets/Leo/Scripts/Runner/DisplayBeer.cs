using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class DisplayBeer : MonoBehaviour
{
    [Header("Level ended settings")] 
    [SerializeField] private TextMeshProUGUI beerTMP;
    private int _countFPS = 30;
    private float _duration = 1f;
    private int _value;

    private void Awake()
    {
        Value = Random.Range(50, 150);
    }

    private Coroutine _countingCoroutine;
    public int Value {
        get => _value;
        set {
            UpdateNumber(value);
            _value = value;
        }
    }
    
    private void UpdateNumber(int value) {
        if (_countingCoroutine != null) {
            StopCoroutine(_countingCoroutine);
        }
        _countingCoroutine = StartCoroutine(CountText(value));
    }

    private IEnumerator CountText(int value)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f / _countFPS);
        int previousValue = _value;

        var stepAmount = value - previousValue < 0 
            ? Mathf.FloorToInt((value - previousValue) / (_countFPS * _duration)) 
            : Mathf.CeilToInt((value - previousValue) / (_countFPS * _duration));

        if (previousValue < value) {
            while (previousValue < value) {
                previousValue += stepAmount;
                if (previousValue > value) previousValue = value;

                beerTMP.SetText(previousValue.ToString());
                
                yield return waitForSeconds;
            }
        }
        else {
            while (previousValue > value) {
                previousValue += stepAmount;
                if (previousValue < value) previousValue = value;

                beerTMP.SetText(previousValue.ToString());
                
                yield return waitForSeconds;
            }
        }
        
    }
}