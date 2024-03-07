using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Manages a road
/// </summary>
public class Road : MonoBehaviour
{
    [SerializeField] private Transform SizeRef;

    private Vector3 _nextposition
    {
        get => transform.position;
        set => transform.position = value;
    }
    
    private void Update() {
        Move();
    }
    
    private void Move()
    {
        _nextposition = new Vector3(_nextposition.x, _nextposition.y, _nextposition.z - RoadsManager.CurrentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Calculates the next's road position 
    /// </summary>
    /// <returns>The position of the next road</returns>
    public Vector3 GetNextPosition()
    {
        float NewSize = SizeRef.GetComponent<BoxCollider>().size.z / 2;
        float CurrentSize = RoadsManager.currentRoad.SizeRef.GetComponent<BoxCollider>().size.z / 2;
        float Offset = NewSize + CurrentSize;
        Debug.Log("Offset = " + Offset);
        return new Vector3(_nextposition.x, _nextposition.y, RoadsManager.currentRoad.transform.position.z + Offset);
    }
}