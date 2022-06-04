using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    public void ChangeVolumeValue()
    {
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", audioSource.volume);
    }
}
