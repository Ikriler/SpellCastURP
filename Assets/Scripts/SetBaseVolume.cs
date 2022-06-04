using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBaseVolume : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("volume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            audioSource.volume = 1;
        }
    }

}
