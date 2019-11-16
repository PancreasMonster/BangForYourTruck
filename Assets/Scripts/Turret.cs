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
    public GameObject currentTarget, ctDir, banana;
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
        if(ctDir != null)
        {
            if (Vector3.Distance(transform.position, ctDir.transform.position) < 100)
            {
                Vector3 dir = ctDir.transform.position - transform.position;
                dir.Normalize();
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(dir.x, dir.y + 90, dir.z)), 5 * Time.deltaTime);
            }
        } 

       if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > 10)
            {
                currentTarget = null;
            }
        }      
    }

    public IEnumerator EnemyCheck()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100, layer);
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
            if (Vector3.Distance(t.transform.position, transform.position) < dist)
            {
                
                dist = Vector3.Distance(t.transform.position, transform.position);
                currentTarget = t;
                ctDir = t;
            }
        }
        if (targets.Count > 0 && currentTarget != null && currentTarget.GetComponent<Health>() != null)
        {
            while ( currentTarget != null && currentTarget.GetComponent<Health>().health >= 0)
            {
                if (!cooldown && currentTarget != null)
                    StartCoroutine(FireBullet(currentTarget));
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
            Vector3 dir = t.transform.position;
            cooldown = true;
            GameObject clone = Instantiate(banana, transform.position, Quaternion.identity);
            clone.GetComponent<BananaMove>().dir = dir;
            clone.GetComponent<BananaMove>().team = GetComponentInParent<Health>().playerNum;
        if (currentTarget != null)
            shootAudio.Play();
            clone.GetComponent<BananaMove>().target = currentTarget;
            yield return new WaitForSeconds(1);
            cooldown = false;
        }
    
}
