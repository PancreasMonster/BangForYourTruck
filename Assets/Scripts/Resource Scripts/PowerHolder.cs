using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerHolder : MonoBehaviour
{
    public GameObject powerDisplay;
    public float powerAmount, maxPower, powerRegen;
    float regenDelay;
    public float regenDelayTime;
    bool infinitePower;
    public float infinitePowerTime;
    public Image[] images;
    public Color col1, col2, col3;

    // Start is called before the first frame update
    void Start()
    {
        images = powerDisplay.GetComponentsInChildren<Image>();
        System.Array.Reverse(images);
        Color imageColor = col1;
        for (int i = 0; i < images.Length; i++)
        {
            if(((float)i / images.Length) < .5f)
            imageColor = Color.Lerp(imageColor, col2, ((float)1 / images.Length * 2));
            else 
            imageColor = Color.Lerp(imageColor, col3, ((float)1 / images.Length * 2));
            images[i].color = imageColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < images.Length; i++)
        {

    
            float f = (maxPower / 100 / images.Length);
            if (((float)i / images.Length) < (1 - (powerAmount / maxPower)))
            {
                images[i].fillAmount = (1 - (powerAmount / maxPower) - (f * i)) / f;
            } else
            {
                images[i].fillAmount = 0;
            }
        }

        
        

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
