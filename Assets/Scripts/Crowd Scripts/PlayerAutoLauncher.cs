﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAutoLauncher : MonoBehaviour
{
    public bool startLaunch = false;
    public float timer;
    public Transform launchPoint;
    public float maxTime = 10;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        if(text != null)
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 && startLaunch)
        {
            timer -= Time.deltaTime;
            text.text = "Auto-Launching in: " + Mathf.CeilToInt(timer).ToString();
        }

        

        if (timer <= 0 && startLaunch)
        {
            GetLaunched();
        }
    }

    public void GetLaunched ()
    {
        startLaunch = false;
        transform.position = launchPoint.position;
        transform.rotation = launchPoint.rotation;
        text.text = "";
    }

    public void StartCountdown ()
    {       
        timer = maxTime;
        startLaunch = true;
    }

    public void StopLaunch()
    {
        startLaunch = false;
        text.text = "";
    }
}