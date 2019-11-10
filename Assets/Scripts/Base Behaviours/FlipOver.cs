using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOver : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask layer;
    public float force, angForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.up, out hit, 10, layer))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * force);
                GetComponent<Rigidbody>().AddTorque(Vector3.right * angForce);
            }
        }
    }
}
