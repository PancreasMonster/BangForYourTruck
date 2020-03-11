using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralPurchaseGate : MonoBehaviour
{
    public int price;
    public int ammoType;
    public int ammoAddition;
    AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {            
            PlayerBank PB = col.GetComponent<PlayerBank>();
            if (PB.tagsInBank >= price)
            {
                col.GetComponent<BuildModeFire>().ammo[ammoType] += ammoAddition;
                PB.tagsInBank -= price;
                audio.Play();

            }           
        }
    }
}
