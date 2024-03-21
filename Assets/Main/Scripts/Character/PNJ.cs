using System;
using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField] private GameObject bubble;
    private int _index;
    
    private void Awake() {
        _index = Random.Range(0, prefabList.Count);
        prefabList[_index].SetActive(true);
    }

    private void Start()
    {
        movebubble();
    }

    private void movebubble()
    {
        float patate = bubble.transform.position.y;
        bubble.transform.DOMoveY(bubble.transform.position.y + 2, 1).onComplete +=
            () => bubble.transform.DOMoveY(patate, 1).onComplete += movebubble;
    }
    public GameObject GetMGPrefab() => mGPrefab;

    public GameObject GetDialogBox() => dialogBox;

    public Sprite GetSprite() => headSprite[_index];
}