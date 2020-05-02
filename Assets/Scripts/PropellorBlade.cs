using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellorBlade : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    float velocity;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = rb.velocity.magnitude;
        //newRotation = (transform.rotation.x, transform.rotation.y, transform.rotation.z);
        transform.Rotate(Vector3.forward * velocity * 10f * Time.deltaTime);
    }
}
