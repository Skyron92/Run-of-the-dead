using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Taptoplay : MonoBehaviour
{
    [SerializeField] InputActionReference input;
    [SerializeField] ZombieProgression zombie;
    [SerializeField] RunnerManager runner;

    // Start is called before the first frame update
    void Start()
    {
        Character.Current.DisableInputs();
        
        input.action.Enable();
        input.action.started += context =>
        {
            Character.Current.EnableInputs();
            RoadsManager.StartMovement();
            RoadsManager.SpeedUp();
            zombie.demarreteszombie();
            runner.startlejeu();
            Destroy(gameObject);
        };
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
