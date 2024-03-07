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

    // Méthode pour récupérer les statistiques de l'item en fonction de son niveau
    public static float GetItemStat(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Cravate:
                return 10f * Mathf.Pow(1.5f, cravateLevel - 1);
            case ItemType.Arme:
                return 20f * Mathf.Pow(1.5f, armeLevel - 1);
            case ItemType.Chaussure:
                return 15f * Mathf.Pow(1.5f, chaussureLevel - 1);
            default:
                Debug.Log("Unknown item type: " + itemType);
                return 0f;
        }
    }
}
public enum ItemType
{
    Cravate,
    Arme,
    Chaussure
}