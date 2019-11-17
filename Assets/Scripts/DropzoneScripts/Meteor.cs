﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float fallSpeed;
    public float force;
    public float maxRange;
    public float damage;
    private Vector3 directionToFall;
    public GameObject powerUp, meteorResource;
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
        Vector3 randCircle = Random.insideUnitCircle * 5;
        Vector3 randCircle2 = Random.insideUnitCircle * 5;
        Instantiate(powerUp, new Vector3(directionToFall.x + randCircle.x, directionToFall.y, directionToFall.z + randCircle.y), Quaternion.identity);
        Instantiate(meteorResource, new Vector3(directionToFall.x + randCircle2.x, directionToFall.y, directionToFall.z + randCircle2.y), Quaternion.identity);
        Destroy(this.gameObject);
    }
}