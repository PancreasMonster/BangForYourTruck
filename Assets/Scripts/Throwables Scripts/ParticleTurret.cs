using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleTurret : AIBehaviours
{
    public LayerMask layer, laserLayer;
    public float targetDist = 5;
    public float damage = 20;
    public float tickRate = 4; // the times per second that the turret checks for a target
    public float range = 100; // max range where units can be spotted and lost
    public GameObject currentTarget; //the game data needed for finding the target via raycast
    public GameObject ctDir; //the game data needed to rotate towards the target
    public Transform firingPoint; // where the bullets and raycast originate from
    bool cooldown;
    List<GameObject> targets = new List<GameObject>();
    public AudioSource shootAudio;
    public float rotationSpeed = 2.5f;

    public ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public LineRenderer lr;
    public GameObject source;

    void Start()
    {
        StartCoroutine(EnemyCheck());
        //ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (ctDir != null)
        {


            if (Vector3.Distance(transform.position, ctDir.transform.position) < range)
            {
                Vector3 dir = ctDir.transform.position - transform.position;
                dir.Normalize();              
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z)), rotationSpeed * Time.deltaTime);
                var psShape = ps.shape;
                psShape.position = firingPoint.localPosition;
                psShape.rotation = dir;
            }
        }

        if (currentTarget != null)
        {

            float magDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (magDist > range)
            {
                currentTarget = null;
            }

            lr.SetPosition(0, firingPoint.position);
            RaycastHit hit;
            if (Physics.Raycast(firingPoint.position, firingPoint.transform.forward, out hit, 10000, laserLayer))
            {
                lr.SetPosition(1, hit.point);
            }
            else
            {
                lr.SetPosition(1, firingPoint.position);
            }


            RaycastHit hit2;
            if (currentTarget != null)
            {

                if (Physics.Raycast(firingPoint.position, (currentTarget.transform.position - firingPoint.position).normalized, out hit2, range, layer))
                {

                    if (hit2.transform.gameObject != currentTarget.gameObject)
                    {
                        currentTarget = null;
                    }
                }

            }
        }
        else
        {
            lr.positionCount = 0;
            ps.Stop();
        }
    }



    public IEnumerator EnemyCheck()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layer);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject)
            {
                if (c.gameObject.GetComponent<Health>().teamNum != GetComponentInParent<Health>().teamNum)
                {
                    targets.Add(c.gameObject);
                }
            }
        }

        float dist = range;
        foreach (GameObject t in targets)
        {
            float magDist = Vector3.Distance(t.transform.position, transform.position);
            if (magDist < dist)
            {
                ctDir = t;
                dist = Vector3.Distance(t.transform.position, transform.position);
                RaycastHit hit;

                if (t.gameObject.tag == "Player")
                {
                    if (Physics.Raycast(firingPoint.position, (new Vector3(t.transform.position.x, t.transform.position.y, t.transform.position.z) - firingPoint.position).normalized, out hit, range, layer))
                    {
                        if (hit.transform.gameObject == t.gameObject)
                        {
                            Debug.Log(hit.transform.name);

                            lr.positionCount = 2;
                            currentTarget = t;
                        }

                        if (hit.transform.gameObject != t.gameObject)
                        {
                            Debug.DrawRay(firingPoint.position, (new Vector3(t.transform.position.x, t.transform.position.y + 1.25f, t.transform.position.z) - firingPoint.position).normalized * hit.distance, Color.blue);
                        }

                    }
                }
                else
                {
                    if (Physics.Raycast(firingPoint.position, (t.transform.position - firingPoint.position).normalized, out hit, range, layer))
                    {
                        if (hit.transform.gameObject == t.gameObject)
                        {
                            //Debug.Log("Found");


                            currentTarget = t;
                        }

                    }
                }

            }
        }
        if (targets.Count > 0 && currentTarget != null && currentTarget.GetComponent<Health>() != null)
        {
            while (currentTarget != null && currentTarget.GetComponent<Health>().health >= 0)
            {

                FireLaser(currentTarget);

                yield return null;
            }

            currentTarget = null;
            targets.Clear();
            yield return new WaitForSeconds(1 / tickRate);

            StartCoroutine(EnemyCheck());
        }
        else
        {
            yield return new WaitForSeconds(1 / tickRate);

            StartCoroutine(EnemyCheck());

        }

    }


    public void FireLaser(GameObject t)
    {
        Vector3 dir = t.transform.position - firingPoint.position;
        dir.Normalize();
        
        if(Vector3.Dot(dir, firingPoint.transform.forward) > .8f)
        {
           if(!ps.isPlaying)
            ps.Play();
        } else
        {
            ps.Stop();
        }

       
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.transform.name);

        if(other.gameObject != this.gameObject && other.GetComponent<Health>())
        other.GetComponent<Health>().TakeDamage("Machine Gunned", source, damage, Vector3.zero);

        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
            }
            i++;
        }
    } 



}

