using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceGravityChanger : MonoBehaviour
{
    public float customGravity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.transform.tag == "Player")
        {
            coll.transform.GetComponent<FlipOver>().gravityForce = customGravity;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.transform.tag == "Player")
        {
            coll.transform.GetComponent<FlipOver>().gravityForce = Mathf.Abs(Physics.gravity.y);
        }
    }
}
