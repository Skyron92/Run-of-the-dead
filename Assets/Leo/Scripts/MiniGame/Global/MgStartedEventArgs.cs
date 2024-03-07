using UnityEngine;

public class MgStartedEventArgs {
    public MgStartedEventArgs(GameObject mGPrefab, GameObject dialogBoxPrefab) {
        MgPrefab = mGPrefab;
        DialogBoxPrefab = dialogBoxPrefab;
    }
    
    public GameObject MgPrefab { get; }
    public GameObject DialogBoxPrefab { get; }
}