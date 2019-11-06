using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour
{
    public float oldVelocity, minimumForce, multAmount;
    public GameObject mbase;
    Rigidbody rb;

   void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldVelocity = rb.velocity.sqrMagnitude;
        minimumForce *= minimumForce;
        StartCoroutine(Collect());
    }


    void FixedUpdate()
    { 
        oldVelocity = rb.velocity.sqrMagnitude;
        
    }

    void Update()
    {
        
    }

    public IEnumerator Collect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4000);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Resource>() != null && c.gameObject != this.gameObject)
            {               
                int collectionAmount = Mathf.RoundToInt(Mathf.Min(5, 40f / Vector3.Distance(transform.position, c.transform.position)));
                c.GetComponent<Resource>().resource -= collectionAmount;
                c.transform.localScale = new Vector3(c.transform.localScale.x, c.transform.localScale.y - (collectionAmount/100f), c.transform.localScale.z);
                c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y - ((collectionAmount/2f)/100f), c.transform.position.z);
                mbase.GetComponent<ResourceHolder>().resourceAmount += collectionAmount;
            }
        }
        yield return new WaitForSeconds(.25f);
        StartCoroutine(Collect());
    }
}
