using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{

    public float healingRate; //healing done per second
    
     void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Health>() != null) //checks for anything with health
        {
            if(other.GetComponent<Health>().health < other.GetComponent<Health>().maxHealth) //if it has health and if then it is below its max, heals by the healingRate
            other.GetComponent<Health>().health += healingRate * Time.deltaTime;
        }
    }
}
