using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : AIBehaviours
{
    public LayerMask layer;
    public float targetDist = 5;
    public float fireRate = 2;
    public float tickRate = 4; // the times per second that the turret checks for a target
    public float range = 100; // max range where units can be spotted and lost
    public float shootAngle = 45; // angle of the mortar strike
    public float mortarSpeed = 10; //speed of the mortar
    public GameObject currentTarget; //the game data needed for finding the target via raycast
    public GameObject ctDir; //the game data needed to rotate towards the target
    public GameObject banana;
    public Transform firingPoint; // where the bullets and raycast originate from
    public bool mortarTurret;
    bool cooldown;
    List<GameObject> targets = new List<GameObject>();
    public AudioSource shootAudio;

    // Start is called before the first frame update
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
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(dir.x, dir.y + 90, dir.z)), 5 * Time.deltaTime);
            }
        }

        if (currentTarget != null)
        {
           // Debug.DrawLine(firingPoint.position, currentTarget.transform.position, Color.red);
            
           

            float magDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (magDist > range)
            {
                currentTarget = null;
            }

            if (!mortarTurret)
            {
                RaycastHit hit;
                if (currentTarget != null)
                {
                    if (currentTarget.gameObject.tag == "Player")
                    {
                        if (Physics.Raycast(firingPoint.position, (new Vector3(currentTarget.transform.position.x, currentTarget.transform.position.y + 0.9824486f, currentTarget.transform.position.z) - firingPoint.position).normalized, out hit, range, layer))
                        {
                            if (hit.transform.gameObject != currentTarget.gameObject)
                            {
                                
                                currentTarget = null;
                                
                            } else
                            {
                                Debug.Log(hit.transform.name);
                                Debug.DrawLine(firingPoint.position, hit.point, Color.green);
                            }
                        }
                    }
                    else
                    {
                        if (Physics.Raycast(firingPoint.position, (currentTarget.transform.position - firingPoint.position).normalized, out hit, range, layer))
                        {
                            if (hit.transform.gameObject != currentTarget.gameObject)
                            {
                                Debug.Log("Lost");
                                currentTarget = null;
                                Debug.Log(hit.transform.name);
                                Debug.DrawLine(firingPoint.position, hit.point, Color.green);
                            }
                        }
                    }
                }
            }
        }
    }

    public IEnumerator EnemyCheck()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layer);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject)
            {
                if (c.gameObject.GetComponent<Health>().playerNum != GetComponentInParent<Health>().playerNum)
                {
                    targets.Add(c.gameObject);
                }
            }
        }
        
        float dist = 1000;
        foreach (GameObject t in targets)
        {
            float magDist = Vector3.Distance(t.transform.position, transform.position);
            if (magDist < dist)
            {
                ctDir = t;
                dist = Vector3.Distance(t.transform.position, transform.position);
                RaycastHit hit;
                if (!mortarTurret)
                {
                    if (t.gameObject.tag == "Player")
                    {
                        if (Physics.Raycast(firingPoint.position, (new Vector3(t.transform.position.x, t.transform.position.y + 0.9824486f, t.transform.position.z) - firingPoint.position).normalized, out hit, range, layer))
                        {
                            if (hit.transform.gameObject == t.gameObject)
                            {
                                Debug.Log(hit.transform.name);


                                currentTarget = t;
                            }

                            if (hit.transform.gameObject != t.gameObject)
                            {
                                Debug.DrawRay(firingPoint.position, (new Vector3(t.transform.position.x, t.transform.position.y + 0.9824486f, t.transform.position.z) - firingPoint.position).normalized * hit.distance, Color.blue);
                            }

                        }
                    }
                    else
                    {
                        if (Physics.Raycast(firingPoint.position, (t.transform.position - firingPoint.position).normalized, out hit, range, layer))
                        {
                            if (hit.transform.gameObject == t.gameObject)
                            {
                                Debug.Log("Found");


                               // currentTarget = t;
                            }

                        }
                    }
                } else
                {
                    currentTarget = t;
                }
            }
        }
        if (targets.Count > 0 && currentTarget != null && currentTarget.GetComponent<Health>() != null)
        {
            while ( currentTarget != null && currentTarget.GetComponent<Health>().health >= 0)
            {
                if (!cooldown && currentTarget != null)
                {
                    if(!mortarTurret)
                        StartCoroutine(FireBullet(currentTarget));
                    if(mortarTurret)
                        StartCoroutine(FireMortar());
                } 
                yield return new WaitForSeconds(fireRate);
            }
            currentTarget = null;
            targets.Clear();
            yield return new WaitForSeconds(tickRate);

            StartCoroutine(EnemyCheck());
        } else {
            yield return new WaitForSeconds(tickRate);
       
            StartCoroutine(EnemyCheck());

        }

    }
        IEnumerator FireBullet(GameObject t)
        {
            Vector3 dir = t.transform.position - firingPoint.position;
            dir.Normalize();
            cooldown = true;
            GameObject clone = Instantiate(banana, firingPoint.position, Quaternion.identity);
            clone.GetComponent<BananaMove>().dir = dir;
            clone.GetComponent<BananaMove>().team = GetComponentInParent<Health>().playerNum;
        if (currentTarget != null)
            shootAudio.Play();
            clone.GetComponent<BananaMove>().target = currentTarget;
            yield return new WaitForSeconds(1);
            cooldown = false;
        }

    IEnumerator FireMortar()
    {
        GameObject clone = Instantiate(banana, firingPoint.position, Quaternion.identity);
        Rigidbody unitRB = clone.GetComponent<Rigidbody>();
        unitRB.velocity = BallisticVel(currentTarget.transform, shootAngle);
        clone.GetComponent<BananaMove>().team = GetComponentInParent<Health>().playerNum;
        if (currentTarget != null)
            shootAudio.Play();
        yield return new WaitForSeconds(1);
        cooldown = false;
    }

    public Vector3 BallisticVel(Transform target, float angle)
    {
        Vector3 targetDir = target.position - firingPoint.position;
        float dist = targetDir.magnitude;
        float radAngle = angle * Mathf.Deg2Rad;
        targetDir.y = dist * Mathf.Tan(radAngle);
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude * mortarSpeed / Mathf.Sin(radAngle * 2));
        return velocity * targetDir.normalized;
    }

}
