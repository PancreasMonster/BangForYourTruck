using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float fallSpeed;
    public float force;
    public float maxRange;
    public float damage;
    public Vector3 directionToFall;
    private bool timeToFall = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToFall)
        transform.position = Vector3.MoveTowards(transform.position, directionToFall, fallSpeed);
        if (Vector3.Distance(transform.position, directionToFall) < 0.1f)
        {
            Explode();
        }
    }

    public void AssignTarget (GameObject target)
    {
        directionToFall = target.transform.position;
        timeToFall = true;
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
