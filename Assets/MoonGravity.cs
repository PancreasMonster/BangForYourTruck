using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGravity : MonoBehaviour
{
    GameObject[] targets;
    Vector3 centerOfGravity;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        centerOfGravity = this.gameObject.transform.position;
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject t in targets)
        {
            Vector3 objectPosition = t.gameObject.transform.position;
            Rigidbody rb = t.GetComponent<Rigidbody>();
            Vector3 directionOfGravity = new Vector3(objectPosition.x - centerOfGravity.x, objectPosition.y - centerOfGravity.y,
                                                        objectPosition.y - centerOfGravity.y).normalized;
            float relativeDistance = directionOfGravity.magnitude;
            rb.AddForce(-directionOfGravity * (force / Mathf.Sqrt(relativeDistance)), ForceMode.Acceleration);
        }
    }
}
