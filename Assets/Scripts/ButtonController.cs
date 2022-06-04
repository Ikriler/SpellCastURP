using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PauseOn(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void SoundTurner(AudioSource music)
    {
        if(music.volume > 0.1)
        {
            music.volume = 0;
            PlayerPrefs.SetFloat("volume", 0);
        }
        else
        {
            music.volume = 1;
            PlayerPrefs.SetFloat("volume", 1);
        }
    }
}
