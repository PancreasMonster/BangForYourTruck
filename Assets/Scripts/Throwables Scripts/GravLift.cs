using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravLift : MonoBehaviour
{
    public float gravForce;
    bool active;
    public float startUpTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Activate", startUpTime);
    }

    void Activate()
    {
        active = true;
    }

    void OnTriggerEnter(Collider other)
    {
        
            if (other.transform.GetComponent<Rigidbody>() != null)
            {
                GetComponent<AudioSource>().Play();
                other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.transform.GetComponent<Rigidbody>().AddForce(transform.forward * gravForce, ForceMode.Impulse);
                Debug.Log("yes");
            }   
    }
}
