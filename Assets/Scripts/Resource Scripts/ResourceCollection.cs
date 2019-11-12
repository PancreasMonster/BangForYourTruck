using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour
{
    public float oldVelocity, minimumForce, multAmount;
    public float collectionRate;
    public float maxRange;
    public GameObject mbase;
    Rigidbody rb;
    ParticleSystem myParticleSystem;
    ParticleSystem.EmissionModule emissionModule;

   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldVelocity = rb.velocity.sqrMagnitude;
        minimumForce *= minimumForce;
        StartCoroutine(Collect());

        myParticleSystem = GetComponentInChildren<ParticleSystem>();
        emissionModule = myParticleSystem.emission;

    }


    void FixedUpdate()
    { 
        oldVelocity = rb.velocity.sqrMagnitude;
        
    }

    void Update()
    {
        
    }

    float SetParticleRateValue(float particleRate)
    {
        emissionModule.rateOverTime = particleRate;
        return particleRate;
    }


    public IEnumerator Collect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRange);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Resource>() != null && c.gameObject != this.gameObject)
            {               
                int collectionAmount = Mathf.RoundToInt(Mathf.Min(5, collectionRate / Vector3.Distance(transform.position, c.transform.position)));
                c.GetComponent<Resource>().resource -= collectionAmount;
                c.transform.localScale = new Vector3(c.transform.localScale.x, c.transform.localScale.y - (collectionAmount/100f), c.transform.localScale.z);
                c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y - ((collectionAmount/2f)/100f), c.transform.position.z);
                mbase.GetComponent<ResourceHolder>().resourceAmount += collectionAmount;
                //SetParticleRateValue(collectionAmount);
            }

            if (hitColliders.Length == 0)
            {
               // SetParticleRateValue(0f);

            }
        }
        yield return new WaitForSeconds(.25f);
        StartCoroutine(Collect());
    }

     void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, maxRange);
    }
}
