using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{


    public float oldVelocity, minimumForce, minimumDamage = 20;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldVelocity = rb.velocity.sqrMagnitude;
        minimumForce *= minimumForce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        oldVelocity = rb.velocity.sqrMagnitude;

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.GetComponent<Health>() != null)
        {
            float damage = Mathf.RoundToInt(Mathf.Min(minimumDamage, oldVelocity / 100));
            coll.transform.GetComponent<Health>().health -= damage;
        }
    }
}
