using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public string volumeParameterName = "MusicVol";

    private void Start()
    {
        if (volumeSlider != null)
            volumeSlider.value = PlayerPrefs.GetFloat(volumeParameterName, 0f);
       
    }
    
    public void SetVolumeLevel()
    {
        Debug.Log(volumeSlider.value);
        audioMixer.SetFloat(volumeParameterName, volumeSlider.value);
        PlayerPrefs.SetFloat(volumeParameterName, volumeSlider.value);
    }
}