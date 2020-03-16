using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserTurret : AIBehaviours
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

    ParticleSystem turretParticles;
    public LineRenderer lr;

    void Start()
    {
       StartCoroutine(EnemyCheck());       
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
        } else
        {
            lr.positionCount = 0;
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
            yield return new WaitForSeconds(1/tickRate);

            StartCoroutine(EnemyCheck());
        }
        else
        {
            yield return new WaitForSeconds(1/tickRate);

            StartCoroutine(EnemyCheck());

        }

    }


    public void FireLaser(GameObject t)
    {
        
        
        RaycastHit hit;
        if (Physics.Raycast(firingPoint.position, firingPoint.transform.forward, out hit, range, layer))
        {
            Debug.Log("Lasering " + hit.transform.name);
            Debug.DrawLine(firingPoint.position, hit.point);
            if (hit.transform.gameObject == t.gameObject)
            {
                t.GetComponent<Health>().health -= damage * Time.deltaTime;
                
            }
           
        }
    }

   

 

}

