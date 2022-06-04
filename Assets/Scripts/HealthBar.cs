using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float smoothing = 0.4f;
    public GameObject _player;
    private Slider slider;
    private void Awake()
    {
        if (_player == null) return;
        slider = GetComponent<Slider>();
        slider.minValue = 8;
        slider.maxValue = _player.GetComponent<Hero>()._life;
    }

    void Update()
    {
        if (_player == null)
        {
            slider.value = Mathf.Lerp(0, slider.value, 0.1f);
            return;
        }
        slider.value = Mathf.Lerp(slider.value, _player.GetComponent<Hero>()._life, Time.deltaTime * smoothing);
    }
}
