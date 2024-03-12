using System;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTMP;
    private string constantTextScore = "Score : ";
    private string scoreDisplay;

    private void OnEnable() {
        scoreTMP.text = constantTextScore + scoreDisplay;
    }
}
