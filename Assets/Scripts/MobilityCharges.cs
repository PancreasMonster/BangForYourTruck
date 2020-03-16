﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilityCharges : MonoBehaviour
{
    public int currentCharges;

    bool charge1;
    public float charge1Time = 0f;
    public SpriteRenderer light1;

    bool charge2;
    public float charge2Time = 0f;
    public SpriteRenderer light2;

    public AudioSource audio;
    bool charge3;
    public float charge3Time = 0f;
    public SpriteRenderer light3;

    public float currentDriftMultiplier;

    public float rechargeTime = 3f;
    public float maxDriftMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        charge1 = true;
        charge2 = true;
        charge3 = true;

        

        currentCharges = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDriftMultiplier > maxDriftMultiplier)
        {
            currentDriftMultiplier = maxDriftMultiplier;
        }

        if (Input.GetKeyDown("z")) {
            UseCharge();
        }

        if (charge1 == false)
        {
            charge1Time += (1 + currentDriftMultiplier) * Time.deltaTime;

        }

        if (charge1Time >= rechargeTime)
        {
            charge1 = true;
            currentCharges++;
            audio.pitch = .9f;
            audio.Play();
            light1.color = Color.green;
            charge1Time = 0f;
        }

        if (charge2 == false)
        {
            charge2Time += (1 + currentDriftMultiplier) * Time.deltaTime;

        }

        if (charge2Time >= rechargeTime)
        {
            charge2 = true;
            currentCharges++;
            audio.pitch = .95f;
            audio.Play();
            light2.color = Color.green;
            charge2Time = 0f;
        }

        if (charge3 == false)
        {
            charge3Time += (1 + currentDriftMultiplier) * Time.deltaTime;

        }

        if (charge3Time >= rechargeTime)
        {
            charge3 = true;
            currentCharges++;
            audio.pitch = 1f;
            audio.Play();
            light3.color = Color.green;
            charge3Time = 0f;
        }

        if (charge1Time >= rechargeTime/2)
        {            
            light1.color = Color.yellow;            
        }

        if (charge2Time >= rechargeTime / 2)
        {
            light2.color = Color.yellow;
        }

        if (charge3Time >= rechargeTime / 2)
        {
            light3.color = Color.yellow;
        }

        if (charge1Time >= (rechargeTime * .25f))
        {
            light1.color = new Color(1.0f, 0.64f, 0.0f);
        }

        if (charge2Time >= (rechargeTime * .25f))
        {
            light1.color = new Color(1.0f, 0.64f, 0.0f);
        }

        if (charge3Time >= (rechargeTime * .25f))
        {
            light1.color = new Color(1.0f, 0.64f, 0.0f);
        }

        if (charge1Time >= (rechargeTime * .75f))
        {
            light1.color = new Color(.64f, 1f, 0.0f);
        }

        if (charge2Time >= (rechargeTime * .75f))
        {
            light1.color = new Color(.64f, 1f, 0.0f);
        }

        if (charge3Time >= (rechargeTime * .75f))
        {
            light1.color = new Color(.64f, 1f, 0.0f);
        }
    }

    public void UseCharge()
    {
        if (currentCharges == 0)
        {
            Debug.Log("empty");
            return;
        }
        else
        {
            ToggleCharges();
            currentCharges--;

        }
    }

    void ToggleCharges()
    {
        if (charge1 == true)
        {
            charge1 = false;
            light1.color = Color.red;
            return;
        }

        if (charge2 == true)
        {
            charge2 = false;
            light2.color = Color.red;
            return;
        }

        if (charge3 == true)
        {
            charge3 = false;
            light3.color = Color.red;
            return;
        }
    }
}