using System;
using System.Collections;
using Tayx.Graphy.Utils.NumString;
using TMPro;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    [SerializeField] private ZombieProgression zombieProgression;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI beerTMP;

    private float _incrementDelay = 0.1f;

    private static int _score;

    private static int _beers;

    private static bool _isEnded;

    public static bool IsEnded() => _isEnded;
    
    private void Start() {
        _isEnded = false;
        zombieProgression.GameOver += OnGameOver;
        Character.Current.Dead += OnGameOver;
        Character.Current.BeerCollected += OnBeerCollected;
    }

    private void OnBeerCollected() {
        _beers++;
        beerTMP.text = _beers.ToString();
    }

    private void Update() {
        StartCoroutine(CountScore());
    }

    public static bool CompareScore() {
        bool bestScore = _score > GameManager.GetScore();
        if(bestScore) GameManager.SetScore(_score);
        return bestScore;
    }
    
    private void OnGameOver() {
        Instantiate(gameOverPanel);
        _isEnded = true;
        GameManager.SetBeerCount(_beers);
        GameManager.SetScore(_score);
        _beers = 0;
        _score = 0;
        CompareScore();
    }

    private void OnGameOver(object sender, EventArgs e) {
        Instantiate(gameOverPanel);
        _isEnded = true;
        GameManager.SetBeerCount(_beers);
        GameManager.SetScore(_score);
        _beers = 0;
        _score = 0;
        CompareScore();
    }

    public static int GetBeerCollected() => _beers; 
    public static int GetScoreReached() => _score; 
    
    //////////////////////////////////////////////////////////
    // !!!! Ajouter les stats du perso !!!!!!!!!!!!!!!!!!!!!!!
    //////////////////////////////////////////////////////////
    private IEnumerator CountScore() {
        _score += (int)(RoadsManager.CurrentSpeed * _incrementDelay);
        yield return new WaitForSeconds(_incrementDelay);
    }
}