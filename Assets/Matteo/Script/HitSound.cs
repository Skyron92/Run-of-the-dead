using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioSource hitSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitSound.Play();
        }
    }
}