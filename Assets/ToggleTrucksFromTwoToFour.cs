using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ToggleTrucksFromTwoToFour : MonoBehaviour
{
    bool twoTrucks;

    public void ToggleTrucks() 
    {
        if (twoTrucks)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(true);
            twoTrucks = false;
        }
        else 
        {
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            twoTrucks = true;
        }
    }
}
