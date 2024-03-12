using System;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    [SerializeField] private ZombieProgression zombieProgression;

    private void Awake()
    {
        zombieProgression.GameOver += OnGameOver;
    }
    
    private void OnGameOver(object sender, EventArgs e) 
    {
        
    }
}