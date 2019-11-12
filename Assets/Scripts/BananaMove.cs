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

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, dir, .25f);
        if (Vector3.Distance(transform.position, dir) < 0.1)
        {
            
            Explode();     
        }

        //if (target == null)
        //    Destroy(this.gameObject);
    }

    public void Explode ()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject)
            {
                if (c.gameObject.GetComponent<Health>().playerNum != team)
                {
                    c.GetComponent<Health>().health -= Mathf.Min( 20, 4f / Vector3.Distance(transform.position,c.transform.position));
                    if(c.GetComponentInChildren<Shaker>() != null)
                    c.GetComponentInChildren<Shaker>().PlayShake();
                }
            }
        }
        BroadcastMessage("SpawnParticle");
        Destroy(this.gameObject);
    }
}
