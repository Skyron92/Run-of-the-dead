using UnityEngine;

/// <summary>
/// Manages a road
/// </summary>
public class Road : MonoBehaviour
{
    /// <summary>
    /// Calculates the next's road position 
    /// </summary>
    /// <returns>The position of the next road</returns>
    public Vector3 GetNextPosition() {
        return new Vector3(transform.position.x, transform.position.y, RoadsManager.currentRoad.transform.position.z + (RoadsManager.currentRoad.transform.localScale.z / 2f + transform.localScale.z / 2f));
    }
}
