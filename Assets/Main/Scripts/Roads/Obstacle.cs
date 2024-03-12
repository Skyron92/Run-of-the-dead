using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    bool isInvuln = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInvuln)
        {
            isInvuln = true;
            var targetspeed = RoadsManager.BaseSpeed;
            RoadsManager.CurrentSpeed = 1;
            // Utilisez la méthode DOVirtual.Float de Dotween pour interpoler la valeur de 1 à 15 sur une durée de 15 secondes
            DOTween.To(() => RoadsManager.CurrentSpeed, x => RoadsManager.CurrentSpeed = x, RoadsManager.BaseSpeed, 1.5f)
                .OnComplete(() => isInvuln = false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
