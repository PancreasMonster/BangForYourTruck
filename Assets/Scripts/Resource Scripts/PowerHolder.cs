using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerHolder : MonoBehaviour
{
    public Image powerDisplay;
    public float powerAmount, maxPower, powerRegen;
    float regenDelay;
    public float regenDelayTime;
    bool infinitePower;
    public float infinitePowerTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        powerDisplay.fillAmount = powerAmount / maxPower;
        powerDisplay.color = new Color(1, powerAmount / maxPower, powerAmount / maxPower, 1);

        if (regenDelay > 0)
            regenDelay -= Time.deltaTime;

        if (powerAmount < maxPower && regenDelay <= regenDelayTime)
        {
            powerAmount += powerRegen * Time.deltaTime;
        }

        if (powerAmount > maxPower)
        {
            powerAmount = maxPower;
        }

        if (infinitePower)
        {
            powerAmount = maxPower;

        }
    }

    public void InfinitePowerOn()
    {

        if (infinitePower == true)
        {
            CancelInvoke();
            Invoke("InfinitePowerOff", infinitePowerTime);
        }

        else {

            infinitePower = true;
            Invoke("InfinitePowerOff", infinitePowerTime);

        }
    }

    public void InfinitePowerOff() {
        infinitePower = false;
    }

    public void losePower (float loss)
    {
        powerAmount -= loss;
        regenDelay = regenDelayTime;
    }
}
