using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundSFXUpdater : MonoBehaviour
{
    [SerializeField] private Sprite full, mid, little;
    [SerializeField] private GameObject shutdown;
    [SerializeField] private Image sfxPicture;

    private void Awake()
    {
        UpdateSFXSprite(-20);
    }

    public void UpdateSFXSprite(float value) {
        switch (value) {
            case >= -20f :
                sfxPicture.sprite = full;
                sfxPicture.enabled = true;
                shutdown.SetActive(false);
                break;
            case < -20f and >= -40f :
                sfxPicture.sprite = mid;
                sfxPicture.enabled = true;
                shutdown.SetActive(false);
                break;
            case < -40f and >= -60f :
                sfxPicture.sprite = little;
                sfxPicture.enabled = true;
                shutdown.SetActive(false);
                break;
            case < -60f and > -80f :
                sfxPicture.enabled = false;
                shutdown.SetActive(false);
                break;
            case <= -80f :
                shutdown.SetActive(true);
                break;
        }
    }
}
