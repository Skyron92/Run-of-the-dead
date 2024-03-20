using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimRun : MonoBehaviour
{
    public Animator drunkRunForward;
    
    private void Update()
    {
        float speedDifference = RoadsManager.CurrentSpeed / RoadsManager.maxSpeed;
    }
    
}
