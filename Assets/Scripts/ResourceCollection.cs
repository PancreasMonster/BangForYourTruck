using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCollection : MonoBehaviour
{
    public float oldVelocity, minimumForce, multAmount;
    public int resourceAmount;
    public Text resourceText;
    Rigidbody rb;

   void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldVelocity = rb.velocity.sqrMagnitude;
        minimumForce *= minimumForce;
    }


    void FixedUpdate()
    { 
        oldVelocity = rb.velocity.sqrMagnitude;
        
    }

    void Update()
    {
        resourceText.text = resourceAmount.ToString();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "Resource")
        {
            if ((oldVelocity - rb.velocity.sqrMagnitude) > minimumForce)
            {
                float calculateGatherAmount = ((oldVelocity - rb.velocity.sqrMagnitude) - minimumForce) / 100;
                resourceAmount += Mathf.RoundToInt(calculateGatherAmount * multAmount);
                coll.transform.localScale = new Vector3(1, coll.transform.localScale.y - ((calculateGatherAmount * multAmount * 2f) /100f), 1);
                coll.transform.position = new Vector3(coll.transform.position.x, coll.transform.position.y - (((calculateGatherAmount * multAmount * 2f) / 2) /100f), coll.transform.position.z);
            }
        }
    }
}
