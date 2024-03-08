using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptions : MonoBehaviour {

    public GameObject Panel;

    public void OpenPanel()
    {
        SlideDetection slideDetection = FindObjectOfType<SlideDetection>();
        slideDetection.DisableSlideInputAction();
        Debug.Log(slideDetection);
        if (Panel != null)
        {
            if (Panel.activeSelf)
            {
                Panel.SetActive(false);
                slideDetection.EnableSlideIpuntAction();
            }
            else
            {
                Panel.SetActive(true);
            }
        }
    }
}
