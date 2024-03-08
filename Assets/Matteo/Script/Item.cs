using UnityEngine;

public class Item
{
    public string name;
    public float baseStat;
    public float upgradeCost;
    public int level;
    
    public Item(string name,int level, float baseStat, float upgradeCost)
    {
        this.name = name;
        this.baseStat = baseStat;
        this.upgradeCost = upgradeCost;
    }
}