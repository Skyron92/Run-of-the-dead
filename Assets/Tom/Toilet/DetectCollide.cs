using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
