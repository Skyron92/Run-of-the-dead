using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollide : MonoBehaviour, IMiniGame
{
    public event IMiniGame.MiniGameWonEvent Victory;
    private int goal = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (goal >= 2)
        {
            Victory?.Invoke(this, MiniGameEventArgs.Empty);
        }
        else
            goal++;
        Debug.Log("collided");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
