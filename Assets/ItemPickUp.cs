using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ParticleSystem ps;
    //ParticleSystem.EmissionModule emissionModule;

    // Start is called before the first frame update
    void Start()
    {
        ps = transform.Find("Item Pickup Particle").GetComponent<ParticleSystem>();
        //emissionModule = ps.emission;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pickup")
        {
            Debug.Log("pickup");
            ps.Play();

        }
    }
}
