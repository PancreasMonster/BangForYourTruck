using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class OptionsSelections : MonoBehaviour
{
    public bool buttonOn;
    public bool hints;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Update()
    {
        if (hints)
        {


            if (PlayerPrefs.GetInt("hintsOn") == 1)
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
        else
        {
            if (PlayerPrefs.GetInt("AVOn") == 1)
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

    public void ToggleButtonText() 
    {
        
    }
}
