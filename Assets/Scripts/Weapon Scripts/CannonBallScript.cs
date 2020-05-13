using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public float reductionFactor;
    public bool destroyOnCollision;
    public float oldVelocity, minimumForce, minimumDamage, maximumDamage;
    public int teamNum;
    public GameObject damageSource; //the source of this GameObject ie. the player that instantiated the 'bullet' 
    public float velocityDamage;
    public float damageToDeal;
    public float cannonAirDamageTimer = 0;
    public Sprite damageImage;

    private Rigidbody rb;
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
        if(cannonAirDamageTimer < 1)
        cannonAirDamageTimer += Time.deltaTime * 2;
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

        damageToDeal = Mathf.Clamp(damageToDeal, 0, float.PositiveInfinity);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.GetComponent<Health>() != null && coll.transform.GetComponent<Health>().teamNum != teamNum)
        {
            
            coll.transform.GetComponent<Health>().TakeDamage(damageImage, damageSource, damageToDeal * cannonAirDamageTimer, Vector3.zero);
        } else if (coll.transform.GetComponentInParent<Health>() != null && coll.transform.GetComponentInParent<Health>().teamNum != teamNum)
        {
            float damage = Mathf.RoundToInt(Mathf.Max(minimumDamage, oldVelocity / 2000));
            coll.transform.GetComponentInParent<Health>().TakeDamage(damageImage, damageSource, damageToDeal * cannonAirDamageTimer, Vector3.zero);
        }

        if (destroyOnCollision)
        {
            Destroy(this.gameObject);
        }
        
    }

   
}

