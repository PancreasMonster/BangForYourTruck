using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ToggleTrucksFromTwoToFour : MonoBehaviour
{

    public void GoToFourTrucks() 
    {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(true);
    }
    public void GoToTwoTrucks()

    {
             transform.GetChild(2).gameObject.SetActive(false);
             transform.GetChild(3).gameObject.SetActive(false);
        
    }
}
