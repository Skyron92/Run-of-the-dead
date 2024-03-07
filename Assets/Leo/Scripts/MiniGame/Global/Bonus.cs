using System;
using UnityEngine;

/// <summary>
/// Parent class of all bonuses
/// </summary>
public abstract class Bonus
{
    /// <summary>
    /// Effects of the bonus
    /// </summary>
    public abstract void Do();
    /// <summary>
    /// Get the direct type of the bonus child
    /// </summary>
    /// <returns></returns>
    public abstract Type GetType();
}

/// <summary>
/// Bike bonus
/// </summary>
public class Bike : Bonus
{
    public override void Do() {
        Debug.Log("ça va tarpin vite !");
    }

    public override Type GetType() {
        return typeof(Bike);
    }
}