using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int beerCount = 0;
    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
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