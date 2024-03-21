using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// PNJ variables
/// </summary>
public class PNJ : MonoBehaviour
{
    [SerializeField] private GameObject mGPrefab;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private List<Sprite> headSprite;
    [SerializeField] private List<GameObject> prefabList;
    private int _index;
    private void Awake() {
        _index = Random.Range(0, prefabList.Count);
        prefabList[_index].SetActive(true);
    }

    public GameObject GetMGPrefab() => mGPrefab;

    public GameObject GetDialogBox() => dialogBox;

    public Sprite GetSprite() => headSprite[_index];
}