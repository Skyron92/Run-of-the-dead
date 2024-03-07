using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour

{

    public static int beerCount = 1000;

    public static int cravateLevel = 1;
    public static int armeLevel = 1;
    public static int chaussureLevel = 1;
    
    public static float GetArmeStat()
    {
        return 20f * Mathf.Pow(1.5f, armeLevel - 1);
    }

    public static float GetChaussureStat()
    {
        return 15f * Mathf.Pow(1.5f, chaussureLevel - 1);
    }
    
    public static float GetCravateStat()
    {
        return 15f * Mathf.Pow(1.5f, cravateLevel - 1);
    }
}
 