using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGravity : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    Vector3 centerOfGravity;
    public float force;
    public float maxDistance;
    float relativeForce;

    // Start is called before the first frame update
    void Start()
    {
        centerOfGravity = this.gameObject.transform.position;
        GameObject[] playerTargets = GameObject.FindGameObjectsWithTag("Player");
        targets.AddRange(playerTargets);
        GameObject[] mineTargets = GameObject.FindGameObjectsWithTag("Mine");
        targets.AddRange(mineTargets);
        relativeForce = force * transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject t in targets)
        {
            float currentDistance = Vector3.Distance(transform.position, t.transform.position);
            Vector3 objectPosition = t.gameObject.transform.position;
            Rigidbody rb = t.GetComponent<Rigidbody>();
            Vector3 directionOfGravity = t.transform.position - transform.position;
            directionOfGravity.Normalize();
            float relativeDistance = directionOfGravity.magnitude;
            if (currentDistance < maxDistance)
            {
                rb.AddForce(-directionOfGravity * (relativeForce * (1 - (currentDistance / maxDistance))), ForceMode.Acceleration);
                Debug.Log("Pulling");
            }
            
        }
    }
}
