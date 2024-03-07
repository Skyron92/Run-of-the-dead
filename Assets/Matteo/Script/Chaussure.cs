using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaussure : MonoBehaviour
{
    public string name;
    public int level;
    public float baseStat;
    public float upgradeCost;
    
    public Chaussure(string name, int level, float baseStat, float upgradeCost)
    {
        this.name = name;
        this.level = level;
        this.baseStat = baseStat;
        this.upgradeCost = upgradeCost;
    }
}
