using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingGate : MonoBehaviour
{
    public int price;

    [FMODUnity.EventRef]
    public string wrench;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {

            PlayerBank PB = col.GetComponent<PlayerBank>();
            if (PB.tagsInBank >= price)
            {
                Health hp = col.GetComponent<Health>();
                if (hp.health < hp.maxHealth)
                {
                    hp.health = hp.maxHealth;
                    PB.tagsInBank -= price;
                    FMODUnity.RuntimeManager.PlayOneShot(wrench);
                }

            }

        }
    }
}
