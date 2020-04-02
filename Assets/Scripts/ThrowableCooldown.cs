﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowableCooldown : MonoBehaviour
{
    public float cooldownTime;
    float t = 0f;
    bool onCooldown;
    float fillAmountValue = 0;
    public Image[] images = new Image[2];

    // Start is called before the first frame update
    void Start()
    {
        images = GetComponentsInChildren<Image>();

        GoOnCooldown();
    }

    // Update is called once per frame
    void Update()
    {
       
        foreach (Image i in images)
        {
            i.fillAmount = fillAmountValue;
        }

        if (onCooldown)
        {
            Debug.Log("LERPING BACK TO COLOR");
            fillAmountValue += Time.deltaTime / cooldownTime;

            if (fillAmountValue >= 1)
            {
                Debug.Log("FINISHED");
                onCooldown = false;
            }
        }
    }

    public void GoOnCooldown()
    {

        Debug.Log("GOING ON COOLDOWN");
        fillAmountValue = 0f;
        onCooldown = true;
    }
}