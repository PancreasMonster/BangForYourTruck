using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public bool resources;
    public int resourceValue;

    public bool healthPack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (resources) {
            if (other.GetComponent<ResourceHolder>())
            {
                ResourceHolder rh = other.GetComponent<ResourceHolder>();
                rh.resourceAmount += resourceValue;
                Destroy(this.gameObject);
            }
        }

        if (healthPack)
        {
            if (other.GetComponent<Health>())
            {
                Health hp = other.GetComponent<Health>();
                hp.health = hp.maxHealth;
                Destroy(this.gameObject);
            }


        }
    }
}
