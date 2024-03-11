using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ZombieProgression zombieProgression;
    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        zombieProgression.GameOver += OnGameOver;
    }

    private void OnGameOver(object sender, EventArgs e) {
        
    }

    public static int beerCount = 1000;
    public static float GetArmeStat()
    {
        return 20f * Mathf.Pow(1.5f,UpgradeArmeSysteme.Current.GetItem().level);
    }

    public static float GetChaussureStat()
    {
        return 15f * Mathf.Pow(1.5f, UpgradeChaussureSysteme.Current.GetItem().level);
    }
    
    public static float GetCravateStat()
    {
        return 10f * Mathf.Pow(1.5f, UpgradeCravateSysteme.Current.GetItem().level);
    }
}
 