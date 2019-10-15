using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : AIBehaviours
{
    public LayerMask layer;
    public float targetDist = 5;
    public Image hp, progress;
    public GameObject currentTarget, banana;
    public ParticleSystem ps;
    bool cooldown;
    List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    { 
        StartCoroutine(EnemyCheck());
    }

    // Update is called once per frame
    void Update()
    {
       if (currentTarget != null)
        {
            Vector3 dir = currentTarget.transform.position - transform.position;
            dir.Normalize();
            transform.rotation = Quaternion.LookRotation(dir);
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > 10)
            {
                currentTarget = null;
            }
        }      
    }

    public IEnumerator EnemyCheck()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10, layer);
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
            }
        }
        if (targets.Count > 0 && currentTarget != null && currentTarget.GetComponent<Health>() != null)
        {
            while ( currentTarget != null && currentTarget.GetComponent<Health>().health >= 0)
            {
                if (!cooldown && currentTarget != null)
                    StartCoroutine(FireBullet(currentTarget));
                yield return new WaitForSeconds(1);
            }
            currentTarget = null;
            targets.Clear();
            yield return new WaitForSeconds(.25f);

            StartCoroutine(EnemyCheck());
        } else {
            yield return new WaitForSeconds(.25f);
       
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
            clone.GetComponent<BananaMove>().target = currentTarget;
            yield return new WaitForSeconds(1);
            cooldown = false;
        }
    
}
