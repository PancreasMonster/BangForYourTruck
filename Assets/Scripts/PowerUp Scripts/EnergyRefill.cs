using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefill : MonoBehaviour
{
    public float powerRegenIncrease = .5f;
    public bool respawn;
    public float respawnTimer;

    Transform child;

    // Start is called before the first frame update
    void Start()
    {
        child = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PowerHolder>() != null)
        {
            if (other.GetComponent<PowerHolder>())
            {
                PowerHolder ph = other.GetComponent<PowerHolder>();
                ph.powerAmount = ph.maxPower;
                ph.powerRegen += powerRegenIncrease;
            }
            if (respawn)
            {
                GetComponent<BoxCollider>().enabled = false;
                child.gameObject.SetActive(false);

                Invoke("Respawn", respawnTimer);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Respawn()
    {
        GetComponent<BoxCollider>().enabled = true;
        child.gameObject.SetActive(true);

    }
}
