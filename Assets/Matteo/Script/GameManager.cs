using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    public static int beerCount = 1000;

    public static float cravateStat = 10f;
    public static float armeStat = 20f;
    public static float chaussureStat = 15f;
    
    public static float cravateUpgradeCost = 100f;
    public static float armeUpgradeCost = 300f;
    public static float chaussureUpgradeCost = 200f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public static void UpgradeItem(string itemName)
    {
        float itemCost;
        float itemStat;

        switch (itemName)
        {
            case "Cravate":
                itemCost = cravateUpgradeCost;
                cravateUpgradeCost *= 1.5f;
                itemStat = cravateStat;
                cravateStat *= 1.5f;
                break;
            case "Arme":
                itemCost = armeUpgradeCost;
                armeUpgradeCost *= 1.5f;
                itemStat = armeStat;
                armeStat *= 1.5f;
                break;
            case "Chaussure":
                itemCost = chaussureUpgradeCost;
                chaussureUpgradeCost *= 1.5f;
                itemStat = chaussureStat;
                chaussureStat *= 1.5f;
                break;
            default:
                Debug.LogError("jsp c quoi : " + itemName);
                return;
        }

        if (beerCount >= itemCost)
        {
            beerCount -= (int)itemCost;
            Debug.Log(itemName + " La nouvelle stats: " + itemStat + ", tarpin cher: " + itemCost);
        }
        else
        {
            Debug.Log("ta pas de biere cheh " + itemName);
        }
    }
}