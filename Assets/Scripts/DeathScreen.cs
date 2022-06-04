using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Text score;
    public Text bestScore;
    public Text currentScore;

    void Update()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScore.text = PlayerPrefs.GetString("bestScore");
        }
        else
        {
            bestScore.text = "0";
        }
    }

    private void OnEnable()
    {
        string bestScorePlayerPrefs;
        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScorePlayerPrefs = PlayerPrefs.GetString("bestScore");
        }
        else
        {
            bestScorePlayerPrefs = "0";
        }

        if (System.Convert.ToUInt32(score.text) > System.Convert.ToUInt32(bestScorePlayerPrefs))
        {
            PlayerPrefs.SetString("bestScore", score.text);
            bestScorePlayerPrefs = score.text;
        }

        bestScore.text = bestScorePlayerPrefs;
        currentScore.text = score.text;
    }
}
