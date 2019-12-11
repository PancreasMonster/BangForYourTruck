using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float fallSpeed;
    public float force;
    public float maxRange;
    public float damage;
    private Vector3 directionToFall;
    public List<GameObject> powerUp = new List<GameObject>();
    public GameObject meteorResource;
    private bool timeToFall = false;
    public GameObject ps;

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
        Vector3 randCircle = Random.insideUnitCircle * 18;
        Vector3 randCircle2 = Random.insideUnitCircle * 5;
        int rand = Random.Range(0, powerUp.Count);
       // Debug.Log(rand);
        GameObject clone = Instantiate(powerUp[rand], new Vector3(directionToFall.x + randCircle.x, directionToFall.y, directionToFall.z + randCircle.y), powerUp[rand].transform.rotation);
        //  Instantiate(meteorResource, new Vector3(directionToFall.x + randCircle2.x, directionToFall.y, directionToFall.z + randCircle2.y), Quaternion.identity);
        // Debug.Log(clone.transform.name);
        Instantiate(ps, transform.position, ps.transform.rotation);
        Destroy(this.gameObject);
        
    }
}
