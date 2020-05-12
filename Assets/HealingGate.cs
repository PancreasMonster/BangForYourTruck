using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingGate : MonoBehaviour
{
    public int price;
    AudioSource audio;

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
                if (col.GetComponent<Health>().health < col.GetComponent<Health>().maxHealth)
                {
                    col.GetComponent<Health>().health = 100;
                    PB.tagsInBank -= price;
                    audio.Play();
                }

            }

        }
    }
}
