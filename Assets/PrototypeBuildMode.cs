using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeBuildMode : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody rb;

    public GameObject capsuleChild;
    public float yRotationSpeed = 5f;
    public float xRotationSpeed = .5f;
    public float maxXRot;
    public float minXRot;


    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetAxis("Mouse X") < 0)
            transform.Rotate((Vector3.up) * -yRotationSpeed);
        if (Input.GetAxis("Mouse X") > 0)
            transform.Rotate((Vector3.up) * yRotationSpeed);

        if (Input.GetKey("a"))
        {
            Debug.Log("a");
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey("d"))
        {
            Debug.Log("d");
            transform.Translate(Vector3.left * -moveSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey("w"))
        {
            Debug.Log("w");
            if (transform.rotation.x <= maxXRot)
            {
                Debug.Log("looking down");


                capsuleChild.transform.Rotate(transform.rotation.x + xRotationSpeed, transform.rotation.y, transform.rotation.z, Space.World);
            }
        }

        if (Input.GetKey("s"))
        {
            if (transform.rotation.x >= minXRot)
            {
                Debug.Log("looking up");

                capsuleChild.transform.Rotate(transform.rotation.x - xRotationSpeed, transform.rotation.y, transform.rotation.z, Space.World);
            }
        }
    }
}
