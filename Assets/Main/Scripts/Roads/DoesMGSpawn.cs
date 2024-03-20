using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoesMGSpawn : MonoBehaviour
{
    private void Awake()
    {
        int prob = Random.Range(0, 10);
        if (prob != 0)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
