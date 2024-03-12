using UnityEngine;

public class ObstacleFatal : MonoBehaviour
{
    bool isInvuln = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInvuln)
        {
            RoadsManager.CurrentSpeed = 0;
            Handheld.Vibrate();
            Debug.Log("You died");
        }
    }
}