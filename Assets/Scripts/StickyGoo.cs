using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyGoo : MonoBehaviour
{
    public float increasedDrag;
    float startingDrag;

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
        if (other.GetComponent<Health>() == true && other.GetComponent<Rigidbody>() == true)
        {
            other.GetComponent<Rigidbody>().drag = increasedDrag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Health>() == true && other.GetComponent<Rigidbody>() == true)
        {
            other.GetComponent<Rigidbody>().drag = 0f;
        }
    }
}
