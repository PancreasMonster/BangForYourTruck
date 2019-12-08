using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravLift : MonoBehaviour
{
    public float gravForce = 10000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
           other.transform.GetComponent<Rigidbody>().AddForce(transform.forward * gravForce);
        }
    }
}
