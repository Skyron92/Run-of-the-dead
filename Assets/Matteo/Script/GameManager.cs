using System;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // Declaration
    private static int _beerCount;
    private static int _cravateLevel;
    private static int _armeLevel;
    private static int _chaussureLevel;
    // Getter
    public static int GetBeerCount() => _beerCount;
    public static int GetCravateLevel() => _cravateLevel;
    public static int GetArmeLevel() => _armeLevel;
    public static int GethaussureLevel() => _chaussureLevel;
    // Setter
    public static void SetBeerCount(int value) => _beerCount = value;
    public static void SetCravateLevel(int value) => _cravateLevel = value;
    public static void  SetArmelevel(int value) => _armeLevel = value;
    public static void SetChaussureLevel(int value) => _chaussureLevel = value;
    
    // Method
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}

//
// public static float GetArmeStat()
// {
//     return 20f * Mathf.Pow(1.5f, UpgradeArmeSysteme.Current.GetItem().level);
// }
//
// public static float GetChaussureStat()
// {
//     return 15f * Mathf.Pow(1.5f, UpgradeChaussureSysteme.Current.GetItem().level);
// }
//
// public static float GetCravateStat()
// {
//     return 10f * Mathf.Pow(1.5f, UpgradeCravateSysteme.Current.GetItem().level);
// }
//