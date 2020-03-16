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
    public GameObject damageSource; //the source of this GameObject ie. the player that instantiated the 'bullet' 
    public float velocityDamage;
    public float damageToDeal;
    public bool player;

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
        if (player)
            damageSource = this.gameObject;
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
                coll.transform.GetComponent<Health>().TakeDamage(null, damageSource, damage, Vector3.zero);
            }
        } else 
        {
            if (!cannonBall && coll.transform.GetComponent<Health>() != null && coll.transform.GetComponent<Health>().teamNum != teamNum)
            {
                float damage = Mathf.RoundToInt(Mathf.Min(minimumDamage, oldVelocity / 100));
                coll.transform.GetComponent<Health>().TakeDamage(null, damageSource, damage, Vector3.zero);
            } 

            if (cannonBall && coll.transform.GetComponent<Health>() != null && coll.transform.GetComponent<Health>().teamNum != teamNum)
            {
                float damage = Mathf.RoundToInt(Mathf.Max(minimumDamage, oldVelocity / 2000));
                coll.transform.GetComponent<Health>().TakeDamage(null, damageSource, damage, Vector3.zero);
               // Debug.Log("Hit");
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

                other.transform.GetComponent<Health>().TakeDamage(null, damageSource, maximumDamage, Vector3.zero);



            }
            else
            {
                other.transform.GetComponent<Health>().TakeDamage(null, damageSource, maximumDamage, Vector3.zero);
            }
        }
    }
}
