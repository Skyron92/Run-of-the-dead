using UnityEngine;

public class MgStartedEventArgs {
    public MgStartedEventArgs(GameObject mGPrefab) {
        MgPrefab = mGPrefab;
    }
    
    public GameObject MgPrefab { get; }
}