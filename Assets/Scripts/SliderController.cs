using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Text valueText;
    public Slider slider;

    public void Start()
    {
        valueText.text = slider.value.ToString();
    }

    public void OnSliderChanged(float value)
    {
        valueText.text = value.ToString();
    }

    
}
