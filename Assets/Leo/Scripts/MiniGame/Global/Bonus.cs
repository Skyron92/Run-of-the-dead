using System;
using UnityEngine;

public abstract class Bonus
{
    public abstract void Do();
    public abstract Type GetType();
}

public class Bike : Bonus
{
    public override void Do() {
        Debug.Log("ça va tarpin vite !");
    }

    public override Type GetType() {
        return typeof(Bike);
    }
}