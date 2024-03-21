using UnityEngine;
public class AnimRun : StateMachineBehaviour
{
    private void Update() {
        float speedDifference = RoadsManager.CurrentSpeed / RoadsManager.maxSpeed;
    }
    
}
