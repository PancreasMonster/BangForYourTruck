using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public bool cannonBall;
    public bool destroyOnCollision;
    public float oldVelocity, minimumForce, minimumDamage = 20;
    public int teamNum;
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
        if (GetComponent<Health>() != null)
        {
            if (coll.transform.GetComponent<Health>() != null && coll.transform.GetComponent<Health>().playerNum != GetComponent<Health>().playerNum)
            {
                float damage = Mathf.RoundToInt(Mathf.Min(minimumDamage, oldVelocity / 100));
                coll.transform.GetComponent<Health>().health -= damage;
            }
        } else 
        {
            if (coll.transform.GetComponent<Health>() != null && coll.transform.GetComponent<Health>().playerNum != teamNum)
            {
                float damage = Mathf.RoundToInt(Mathf.Min(minimumDamage, oldVelocity / 100));
                coll.transform.GetComponent<Health>().health -= damage;
            }

            if (cannonBall)
            {
                float damage = Mathf.RoundToInt(Mathf.Max(minimumDamage, oldVelocity / 2000));
                coll.transform.GetComponent<Health>().health -= damage;
            }
        }

        if (destroyOnCollision)
        {
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
         if (other.transform.GetComponent<Health>() != null && other.transform.GetComponent<Health>().playerNum != teamNum)
         {

             float damage = Mathf.RoundToInt(Mathf.Max(minimumDamage, oldVelocity / 300));
             other.transform.GetComponent<Health>().health -= damage;
            
        }
    }
}
