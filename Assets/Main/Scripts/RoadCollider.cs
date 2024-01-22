using UnityEngine;

/// <summary>
/// Manages the road's spawn call
/// </summary>
/// RequireComponent force the game object to have a collider component
[RequireComponent (typeof(Collider))]
public class RoadCollider : MonoBehaviour
{
    private void Awake() {
        // Set the trigger parameter of the collision on true
        GetComponent<Collider>().isTrigger = true;
    }
    // Detect a trigger collision
    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with this (the road collider)
        if (other.gameObject.CompareTag("Player")) {
            // Requests the RoadsManager to spawn the next road
            RoadsManager.SpawnNextRoad();
        }
    }
}
