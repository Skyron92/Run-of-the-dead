using UnityEngine;

public class MgStartedEventArgs {
    public MgStartedEventArgs(GameObject mGPrefab, GameObject dialogBoxPrefab) {
        MgPrefab = mGPrefab;
        DialogBoxPrefab = dialogBoxPrefab;
    }
    
    public MgStartedEventArgs(GameObject mGPrefab, GameObject dialogBoxPrefab, Sprite sprite) {
        MgPrefab = mGPrefab;
        DialogBoxPrefab = dialogBoxPrefab;
        Headsprite = sprite;
    }
    
    public GameObject MgPrefab { get; }
    public GameObject DialogBoxPrefab { get; }
    
    public Sprite Headsprite { get; }
}