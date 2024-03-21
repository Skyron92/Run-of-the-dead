using System;
using UnityEngine;

public class Item
{
    public static int GetCost(ItemType type)
    {
        switch (type)
        {
            case ItemType.Arme:
                return (int)(20f * Mathf.Pow(1.5f, GameManager.GetArmeLevel()));
            case ItemType.Chaussure:
                return (int)(15f * Mathf.Pow(1.5f, GameManager.GetChaussureLevel()));
            case ItemType.Cravate:
                return (int)(10f * Mathf.Pow(1.5f, GameManager.GetCravateLevel()));
            default:
                return -1;
        }
    }
}


