using System;
using UnityEngine;

public class Item
{
    public static float GetCost(ItemType type)
    {
        switch (type)
        {
            case ItemType.Arme:
                return 20f * Mathf.Pow(1.5f, GameManager.GetArmeLevel());
            case ItemType.Chaussure:
                return 15f * Mathf.Pow(1.5f, GameManager.GetChaussureLevel());
            case ItemType.Cravate:
                return 10f * Mathf.Pow(1.5f, GameManager.GetCravateLevel());
            default:
                return -1;
        }
    }
}


