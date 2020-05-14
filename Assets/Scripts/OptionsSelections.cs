using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class OptionsSelections : MonoBehaviour
{
    public bool buttonOn;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Update()
    {
        
    }

    public void ToggleButtonText() 
    {
        if (buttonOn)
        {
            buttonOn = false;
            transform.GetChild(0).GetComponent<Text>().text = "Off";
            transform.GetChild(1).GetComponent<Text>().text = "Off";

        }
        else 
        {
            buttonOn = true;
            transform.GetChild(0).GetComponent<Text>().text = "On";
            transform.GetChild(1).GetComponent<Text>().text = "On";
        }
    }
}
