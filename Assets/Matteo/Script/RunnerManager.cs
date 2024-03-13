using System;
using System.Collections;
using Tayx.Graphy.Utils.NumString;
using TMPro;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    [SerializeField] private ZombieProgression zombieProgression;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreDisplayText;

    private float _incrementDelay;

    private int _score;

    private void Awake() {
        zombieProgression.GameOver += OnGameOver;
    }

    private void Update() {
        StartCoroutine(CountScore());
    }

    private void OnGameOver(object sender, EventArgs e) {
        gameOverPanel.SetActive(true); 
    }

    //////////////////////////////////////////////////////////
    // !!!! Ajouter les stats du perso !!!!!!!!!!!!!!!!!!!!!!!
    //////////////////////////////////////////////////////////
    private IEnumerator CountScore() {
        _score += (RoadsManager.CurrentSpeed.ToInt() * _incrementDelay).ToInt();
        yield return new WaitForSeconds(_incrementDelay);
    }
}