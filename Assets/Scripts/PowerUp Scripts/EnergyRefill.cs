using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefill : MonoBehaviour
{
    public float powerRegenIncrease = .5f;

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
        if (other.GetComponent<PowerHolder>())
        {
            PowerHolder ph = other.GetComponent<PowerHolder>();
            ph.powerAmount = ph.maxPower;
            ph.powerRegen += powerRegenIncrease;
            Destroy(this.gameObject);
        }
    }
}
