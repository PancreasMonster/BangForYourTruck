using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerHolder : MonoBehaviour
{
    public Image powerDisplay;
    public float powerAmount, maxPower, powerRegen;
    float regenDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        powerDisplay.fillAmount = powerAmount / maxPower;

        if (regenDelay > 0)
            regenDelay -= Time.deltaTime;

        if (powerAmount < maxPower && regenDelay <= 0)
        {
            powerAmount += powerRegen * Time.deltaTime;
        }

        if (powerAmount > maxPower)
        {
            powerAmount = maxPower;
        }
    }

    public void losePower (float loss)
    {
        powerAmount -= loss;
        regenDelay = 2;
    }
}
