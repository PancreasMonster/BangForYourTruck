﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform target;
    public GameObject particles;
    public GameObject source;
    public int teamNum;

    public float speed = 500f; // speed of the missle forward
    public float rotateSpeed = 200f; // how quickly the missile turns to target 
    public float waitTimeForHoming = 1f; // if searching for a target, how long it takes to take to do a overlap sphere check. if you already have a target, how long to start homing
    public float launchForce = 1000f; // inital impulse force on instantiation
    public float checkRange = 500f; // how far the missle 
    public float damageRange = 75f;
    public float maxDamage = 80f;

    private bool lockedOn = false;

    private Rigidbody rb;

    bool exploded;

    private List<GameObject> playersHit = new List<GameObject>();

    public Sprite damageImage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 60);
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitForHoming());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockedOn)
        {
            if (target)
            {
                Vector3 dir = target.position - rb.position;

                dir.Normalize();

                Vector3 rotateAmount = Vector3.Cross(transform.forward, dir);

                rb.angularVelocity = rotateAmount * rotateSpeed;
            }
                rb.velocity = transform.forward * speed;
            
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (!exploded)
        {
            Instantiate(particles, transform.position, transform.rotation);
            StopAllCoroutines();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRange);
            foreach (Collider c in hitColliders)
            {
                if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject)
                {
                    if (c.gameObject.GetComponent<Health>().teamNum != teamNum)
                    {
                        if (Vector3.Distance(transform.position, c.transform.position) > damageRange / 2f)
                        {
                            c.GetComponent<Health>().TakeDamage(damageImage, source, maxDamage / 2f, Vector3.zero);
                            Debug.Log(maxDamage / 2f);
                            Debug.Log(c.transform.name);
                        }
                        else if (Vector3.Distance(transform.position, c.transform.position) <= damageRange / 2f)
                        {
                            c.GetComponent<Health>().TakeDamage(damageImage, source, maxDamage, Vector3.zero);
                            Debug.Log(maxDamage);
                            Debug.Log(c.transform.name);
                        }
                    }
                }
            }

            Destroy(this.gameObject);
            exploded = true;
        }
    }

    private IEnumerator WaitForHoming ()
    {

            rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
            yield return new WaitForSeconds(waitTimeForHoming);
            GetComponent<AudioSource>().Play();
            lockedOn = true;     
            if(!target)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRange);
            List<Transform> targets = new List<Transform>();
            foreach (Collider c in hitColliders)
            {
                if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject && c.transform.tag != "Drone")
                {
                    if (c.gameObject.GetComponent<Health>().teamNum != teamNum)
                    {
                        targets.Add(c.transform);
                    }
                }
            }

            float dist = float.PositiveInfinity;

            foreach (Transform t in targets)
            {
                float magDist = Vector3.Distance(t.transform.position, transform.position);
                if (magDist < dist)
                {
                    dist = magDist;
                    target = t;
                }
            }
        }
            if(target == null)
        {
            StartCoroutine(CheckForTargets());
        }
    }

    private IEnumerator CheckForTargets()
    {

        yield return new WaitForSeconds(.5f);
        if (!target)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRange);
            List<Transform> targets = new List<Transform>();
            foreach (Collider c in hitColliders)
            {
                if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject && c.transform.tag != "Drone")
                {
                    if (c.gameObject.GetComponent<Health>().teamNum != teamNum)
                    {
                        targets.Add(c.transform);
                    }
                }
            }

            float dist = float.PositiveInfinity;

            foreach (Transform t in targets)
            {
                float magDist = Vector3.Distance(t.transform.position, transform.position);
                if (magDist < dist)
                {
                    dist = magDist;
                    target = t;
                }
            }
        }
        if (target == null)
        {
            StartCoroutine(CheckForTargets());
        }
    }
}
