using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject target;
    public Vector3 dir;
    public int team;
    public float range = 10;
    public float maxDamage = 20;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, dir, 1f);
        

        //if (target == null)
        //    Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    public void Explode ()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject)
            {
                if (c.gameObject.GetComponent<Health>().playerNum != team)
                {
                    if (Vector3.Distance(transform.position, c.transform.position) > range / 2f) {
                        c.GetComponent<Health>().health -= maxDamage / 2f;
                            }
                    else if (Vector3.Distance(transform.position, c.transform.position) <= range / 2f)
                    {
                        .GetComponent<Health>().health -= maxDamage;
                    }
                    if(c.GetComponentInChildren<Shaker>() != null)
                    c.GetComponentInChildren<Shaker>().PlayShake();
                }
            }
        }
        BroadcastMessage("SpawnParticle");
        Destroy(this.gameObject);
    }
}
