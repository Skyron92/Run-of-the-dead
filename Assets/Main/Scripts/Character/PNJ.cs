using System;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    [SerializeField] private GameObject mGPrefab;
    [SerializeField] private GameObject dialogBox;
    
    public GameObject GetMGPrefab() {
        return mGPrefab;
    }
    
    public GameObject GetDiologBox() {
        return dialogBox;
    }
}