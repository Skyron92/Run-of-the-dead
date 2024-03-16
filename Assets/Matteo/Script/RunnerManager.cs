﻿using System;
using System.Collections;
using Tayx.Graphy.Utils.NumString;
using TMPro;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    [SerializeField] private ZombieProgression zombieProgression;

    [SerializeField] private GameObject gameOverPanel;

    private float _incrementDelay;

    private int _score;

    private static int _beers;

    private void Start() {
        zombieProgression.GameOver += OnGameOver;
        Character.Current.BeerCollected += OnBeerCollected;
    }

    private void OnBeerCollected() {
        _beers++;
    }

    private void Update() {
        StartCoroutine(CountScore());
    }

    private void OnGameOver(object sender, EventArgs e) {
        gameOverPanel.SetActive(true); 
        GameManager.SetBeerCount(_beers);
        GameManager.SetScore(_score);
    }

    //////////////////////////////////////////////////////////
    // !!!! Ajouter les stats du perso !!!!!!!!!!!!!!!!!!!!!!!
    //////////////////////////////////////////////////////////
    private IEnumerator CountScore() {
        _score += (RoadsManager.CurrentSpeed.ToInt() * _incrementDelay).ToInt();
        yield return new WaitForSeconds(_incrementDelay);
    }
}