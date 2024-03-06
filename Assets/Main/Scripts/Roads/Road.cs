using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages a road
/// </summary>
public class Road : MonoBehaviour
{
    [SerializeField] private Transform EndRoadRef;
    private void Update() {
        Move();
    }
    
    private void Move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - RoadsManager.Speed * Time.deltaTime);
    }

    /// <summary>
    /// Calculates the next's road position 
    /// </summary>
    /// <returns>The position of the next road</returns>
    public Vector3 GetNextPosition() 
    {
        return new Vector3(transform.position.x, transform.position.y, Vector3.Distance(RoadsManager.currentRoad.EndRoadRef.position, RoadsManager.currentRoad.transform.position));
    }
}
