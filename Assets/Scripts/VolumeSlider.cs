using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = this.GetComponent<Slider>();
        if (PlayerPrefs.HasKey("volume"))
        {
            slider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            slider.value = 1;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            slider.value = PlayerPrefs.GetFloat("volume");
        }
    }
}
