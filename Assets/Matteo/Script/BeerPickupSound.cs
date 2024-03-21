using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerPickupSound : MonoBehaviour
{
    public AudioSource beerSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            beerSound.Play();
        }
    }
}