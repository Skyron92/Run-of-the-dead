using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Taptoplay : MonoBehaviour
{
    [SerializeField] InputActionReference input;
    
    // Start is called before the first frame update
    void Start()
    {
        input.action.Enable();
        input.action.started += context => Destroy(gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
