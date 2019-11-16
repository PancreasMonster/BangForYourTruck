using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float fallSpeed;
    public float force;
    public float maxRange;
    public float damage;
    private Vector3 directionToFall = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, directionToFall, fallSpeed);
        if (Vector3.Distance(transform.position, directionToFall) < 0.1f)
        {
            Explode();
        }
    }

    public void AssignTarget (GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        directionToFall = dir;
    }

    public void Explode()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRange); //gets an array of all the colliders within maxRange units
        foreach (Collider c in hitColliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(force * rb.mass, transform.position, maxRange);

            Health h = c.GetComponent<Health>();
            if (h != null)
                h.health -= damage;
        }
        Destroy(this.gameObject);
    }
}
