using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonSpaws : MonoBehaviour
{
    public Slider slider;
    public Sprite[] sprites;
    Image currentImage;
    private void Awake()
    {
        currentImage = this.GetComponent<Image>();
    }

    void Update()
    {
        if (slider.value == 0)
        {
            currentImage.sprite = sprites[0];
        }
        else
        {
            currentImage.sprite = sprites[1];
        }
    }
}
