﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoicelinesMixer : MonoBehaviour
{
    Slider slider;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float ppValue = PlayerPrefs.GetFloat("VoicelinesMixer", value);
        slider.value = ppValue;
    }

    // Update is called once per frame
    void Update()
    {
        value = sliderValue();
        PlayerPrefs.SetFloat("VoicelinesMixer", value);
    }

    public float sliderValue()
    {
        return slider.value;
    }
}

