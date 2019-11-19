using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour
{
    public float oldVelocity, minimumForce, multAmount;
    public float collectionRate;
    public float maxRange;
    public float finalCollectionAmount;
    public GameObject mbase;
    Rigidbody rb;
    public ParticleSystem myParticleSystem;
    ParticleSystem.EmissionModule emissionModule;

   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldVelocity = rb.velocity.sqrMagnitude;
        minimumForce *= minimumForce;
        StartCoroutine(Collect());


       // myParticleSystem = GetComponentInChildren<ParticleSystem>();
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
        emissionModule = myParticleSystem.emission;

        emissionModule.rateOverTime = particleRate;
        return particleRate;
    }


    public IEnumerator Collect()
    {
        float addedCollectionAmount = 0f;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRange);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Resource>() != null && c.gameObject != this.gameObject)
            {
                
                int collectionAmount = Mathf.RoundToInt(Mathf.Min(5, collectionRate / Vector3.Distance(transform.position, c.transform.position)));
                c.GetComponent<Resource>().resource -= collectionAmount;
                c.transform.localScale = new Vector3(c.transform.localScale.x, c.transform.localScale.y - (collectionAmount/c.GetComponent<Resource>().resource), c.transform.localScale.z);
                c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y - ((collectionAmount/2f)/ c.GetComponent<Resource>().resource), c.transform.position.z);
                if (c.transform.localScale.y <= 0)
                    Destroy(c.gameObject);
                mbase.GetComponent<ResourceHolder>().resourceAmount += collectionAmount;
                addedCollectionAmount += collectionRate;
            }



            if (hitColliders.Length == 0)
            {
               SetParticleRateValue(0f);

            }        
        }

        SetParticleRateValue(addedCollectionAmount);

        yield return new WaitForSeconds(.25f);
        StartCoroutine(Collect());
    }

     void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, maxRange);
    }
}
