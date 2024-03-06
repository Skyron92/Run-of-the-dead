using UnityEngine;

public class PNJ : MonoBehaviour
{
    [SerializeField] private GameObject mGPrefab;
    
    public GameObject GetMGPrefab() {
        return mGPrefab;
    }
}