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
}

/// <summary>
/// Bike bonus
/// </summary>
public class Bike : Bonus
{
    public override void Do() {
        RoadsManager.SpeedUp(RoadsManager.CurrentSpeed + 20);
        Character.Current.IsBoosted = true;
    }

    public Type GetType() {
        return typeof(Bike);
    }
}