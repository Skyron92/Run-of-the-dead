using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the roads spawn
/// </summary>
public class RoadsManager : MonoBehaviour
{
    public static List<GameObject> RoadsList = new List<GameObject>();
    [SerializeField] private List<GameObject> initRoadsList = new List<GameObject>();
    public static Road currentRoad;
    [SerializeField] private Road StartRoad;

    public static float CurrentSpeed = BaseSpeed;
    private const float BaseSpeed = 15f;

    private void Awake() {
        // Initialize the current road
        currentRoad = StartRoad;

        // Initiialize the road list
        for (int i = 0; i < initRoadsList.Count; i++) {
            RoadsList.Add(initRoadsList[i]);
        }
    }

    /// <summary>
    /// Spawn a random road next to the current road
    /// </summary>
    public static void SpawnNextRoad()
    {
        // Get a random index in the range of the RoadsList
        int index = Random.Range(0, RoadsList.Count);

        // Instantiate a road and save it in a variable
        var instance = Instantiate(RoadsList[index]);
        Road myRoad = instance.GetComponent<Road>();
        // Set the position of the road
        instance.transform.position = myRoad.GetNextPosition();

        // Update the new current road
        currentRoad = myRoad;
    }

    public static void StopMovement() {
        CurrentSpeed = 0;
    }

    public static void StartMovement() {
        CurrentSpeed = BaseSpeed;
    }
    
}