using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseGate : MonoBehaviour
{
    public int price;
    public int ammoType;
    public int ammoAddition;
    public int teamGateNum;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "Player")
        {
            if (col.GetComponent<Health>().teamNum == teamGateNum)
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
}