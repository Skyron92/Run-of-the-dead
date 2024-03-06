using UnityEngine;

public class Item
{
    public string name;
    public float baseStat;
    public float upgradeCost;

    // Constructeur de la classe Item
    public Item(string name, float baseStat, float upgradeCost)
    {
        this.name = name;
        this.baseStat = baseStat;
        this.upgradeCost = upgradeCost;
    }
}