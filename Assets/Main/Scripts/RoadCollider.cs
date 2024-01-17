using UnityEngine;

/// <summary>
/// Manages the road's spawn call
/// </summary>
public class RoadCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RoadsManager.SpawnNextRoad();
        }
    }
}
