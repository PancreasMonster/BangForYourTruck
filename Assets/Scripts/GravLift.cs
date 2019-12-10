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
        if(other.transform.GetComponent<Rigidbody>() != null)
        {
            other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.GetComponent<Rigidbody>().AddForce(transform.forward * gravForce * (other.transform.GetComponent<Rigidbody>().mass/18));
        }
    }
}
