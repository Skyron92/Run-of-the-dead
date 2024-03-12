using System;
using System.Collections;
using Tayx.Graphy.Utils.NumString;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    [SerializeField] private ZombieProgression zombieProgression;

    [SerializeField] private GameObject gameOverPanel;

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

    private IEnumerator CountScore() {
        _score += RoadsManager.CurrentSpeed.ToInt();
        yield return new WaitForSeconds(.1f);
    }
}