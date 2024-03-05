using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages a road
/// </summary>
public class Road : MonoBehaviour
{
    private void Update() {
        Move();
    }

    private void Move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y,
            transform.position.z - RoadsManager.Speed * Time.deltaTime);
    }

    /// <summary>
    /// Calculates the next's road position 
    /// </summary>
    /// <returns>The position of the next road</returns>
    public Vector3 GetNextPosition() {
        return new Vector3(transform.position.x, transform.position.y, RoadsManager.currentRoad.transform.position.z + (RoadsManager.currentRoad.transform.localScale.z / 2f + transform.localScale.z / 2f));
    }
}
