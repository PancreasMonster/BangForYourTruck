using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : AIBehaviours
{
    public LayerMask layer;
    public float targetDist = 5;
    public Image hp, progress;
    public GameObject hpBar, progressBar, currentTarget, banana;
    public ParticleSystem ps;
    bool cooldown;
    List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        NMA.enabled = false;
        NMA.speed = speed;
        StartCoroutine(EnemyCheck());
    }

    // Update is called once per frame
    void Update()
    {
        if (!inAir)
        {
          //  if (Vector3.Distance(transform.position, target.transform.position) > targetDist)
          //  {
         //       NMA.destination = target.transform.position;
         //   }
             if (currentTarget == null)
            {
                //NMA.destination = transform.position;
                // target.GetComponent<BaseHealth>().health -= 10 * Time.deltaTime;
                // if (!cooldown && currentTarget != null)
                //    StartCoroutine(FireBanana(target));
            }
            //hp.fillAmount = health / maxhealth;
        }
        //if (health <= 0)
        //    Destroy(this.gameObject);
    } 

    /* void OnCollisionEnter(Collision col)
     {
         if (col.transform.tag == "Plane" && inAir)
         {
             NMA.enabled = true;
             inAir = false;
             gameObject.layer = 11;
             GetComponent<Rigidbody>().isKinematic = true;
             hpBar.SetActive(true);
             FindEnemyBase();
             StartCoroutine(EnemyCheck());
         }
     }*/

    // void FindEnemyBase()
    //{
    //      target = homeBase.GetComponent<FireCannon>().enemyBase.gameObject;
    // }

    public IEnumerator EnemyCheck()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10, layer);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.tag == "Resource")
            {
                targets.Add(c.gameObject);
                Debug.Log(c.transform.name);
            }
        }
        float dist = 100;
        foreach (GameObject t in targets)
        {
            if (Vector3.Distance(t.transform.position, transform.position) < dist)
            {
                dist = Vector3.Distance(t.transform.position, transform.position);
                currentTarget = t;
            }
        }
         if (currentTarget != null && currentTarget.GetComponent<Health>() != null)
        {
            while (currentTarget.GetComponent<Health>().health >= 0)
            {
               // NMA.destination = transform.position;
                currentTarget.GetComponent<Health>().health -= 25 * Time.deltaTime;
                if (!cooldown && currentTarget != null)
                    StartCoroutine(FireBanana(currentTarget));
                yield return null;
            }
            currentTarget = null;
            targets.Clear();
            //  } 

            /*  if (currentTarget != null && currentTarget.GetComponent<Attacker>() != null)
              {

                  while (currentTarget.GetComponent<Attacker>().health >= 0)
                  {
                      NMA.destination = transform.position;
                      currentTarget.GetComponent<Attacker>().health -= 25 * Time.deltaTime;
                      if (!cooldown && currentTarget != null)
                          StartCoroutine(FireBanana(currentTarget));
                      yield return null;
                  }
                  currentTarget = null;
                  targets.Clear();
              } */
            yield return new WaitForSeconds(.25f);
            Debug.Log("Check");
            StartCoroutine(EnemyCheck());
        }

        IEnumerator FireBanana(GameObject t)
        {
            Vector3 dir = t.transform.position - transform.position;
            dir.Normalize();
            cooldown = true;
            GameObject clone = Instantiate(banana, transform.position, Quaternion.identity);
            clone.GetComponent<BananaMove>().dir = dir;
            yield return new WaitForSeconds(1);
            cooldown = false;
        }
    }
}
