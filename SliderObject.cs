using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderObject : MonoBehaviour
{
    public Slider Slider;
    public TMP_Text ValueText;

    public SliderType SliderType;

    public void Awake()
    {
        switch (SliderType)
        {
            case SliderType.Volume:
                Slider.value = (int)(Options.Volume * 100);
                ValueText.text = $"{(int)(Options.Volume * 100)}%";
                return;
            case SliderType.MouseSensitivity:
                Slider.value = Options.MouseSensitivity;
                ValueText.text = $"{Options.MouseSensitivity} °/ΔpxΔt";
                return;
        }
    }
}

public enum SliderType : int
{
    Normal = 0,
    Volume,
    MouseSensitivity
}