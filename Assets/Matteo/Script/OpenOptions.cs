using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptions : MonoBehaviour
{

    [SerializeField] private SlideDetection slideDetection;

    public void OpenPanel(GameObject Panel) {
        if (Panel != null) {
            if (Panel.activeSelf) {
                Panel.SetActive(false);
                slideDetection.EnableSlideIpuntAction();
            }
            else {
                slideDetection.DisableSlideInputAction();
                Panel.SetActive(true);
            }
        }
    }
}
