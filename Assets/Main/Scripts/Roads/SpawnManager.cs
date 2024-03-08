using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class SpawnManager : MonoBehaviour
{   
    int _tab;
    GameObject[] all_spawn;

    // Find all objects with tag SpawnPoint and put them in a tab respecting their order in the hierarchy 
    void InitialiseSpawnTab()
    {
        all_spawn = GameObject.FindGameObjectsWithTag("SpawnPoint").OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
    }
    
    
// Loop through the transforms in the scene, exporting what is necessary
    //     foreach (Transform t in all_transforms)
    // {
    //     //do save etc.
    // }
        
}
