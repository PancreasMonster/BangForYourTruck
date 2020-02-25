using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public float reductionFactor;
    public bool cannonBall;
    public bool destroyOnCollision;
    public float oldVelocity, minimumForce, minimumDamage, maximumDamage;
    public int teamNum;

    public float velocityDamage;
    public float damageToDeal;

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
        velocityDamage = oldVelocity / reductionFactor;
        if (minimumDamage > velocityDamage)
        {
            damageToDeal = minimumDamage;

        }
        else
        {
            damageToDeal = velocityDamage;

        }

        if (velocityDamage > maximumDamage)
        {
            damageToDeal = maximumDamage;

        }
        else
        {
            damageToDeal = velocityDamage;

        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (GetComponent<Health>() != null)
        {
            if (coll.transform.GetComponent<Health>() != null && coll.transform.GetComponent<Health>().teamNum != GetComponent<Health>().playerNum)
            {
                float damage = Mathf.RoundToInt(Mathf.Min(minimumDamage, oldVelocity / 100));
                coll.transform.GetComponent<Health>().health -= damage;
            }
        } else 
        {
           /* if (coll.transform.GetComponent<Health>() != null && coll.transform.GetComponent<Health>().teamNum != teamNum)
            {
                float damage = Mathf.RoundToInt(Mathf.Min(minimumDamage, oldVelocity / 100));
                coll.transform.GetComponent<Health>().health -= damage;
            } */

            if (cannonBall && coll.transform.GetComponent<Health>() != null && coll.transform.GetComponent<Health>().teamNum != teamNum)
            {
                float damage = Mathf.RoundToInt(Mathf.Max(minimumDamage, oldVelocity / 2000));
                coll.transform.GetComponent<Health>().health -= damage;
                Debug.Log("Hit");
            }
        }

        if (destroyOnCollision)
        {
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
         if (other.transform.GetComponent<Health>() != null && other.transform.GetComponent<Health>().teamNum != teamNum)
         {

             float damage = Mathf.RoundToInt(Mathf.Max(minimumDamage, oldVelocity / reductionFactor));
            if (damage >= maximumDamage)
            {

                other.transform.GetComponent<Health>().health -= maximumDamage;



            }
            else
            {
                other.transform.GetComponent<Health>().health -= damage;
            }
        }
    }
}
