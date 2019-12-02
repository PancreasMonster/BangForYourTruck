using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeBuildMode : MonoBehaviour
{
    public float vertMoveSpeed;
    public float horizMoveSpeed;
    Rigidbody rb;
    Vector3 startRotation;
    float camVertSpeed = 1f;

    public GameObject vCam;
    public GameObject vCamTarget;
    public GameObject capsuleChild;
    public float yRotationSpeed = 5f;
    public float xRotationSpeed = .5f;
    public float maxXRot;
    public float minXRot;


    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      startRotation = new Vector3(vCamTarget.transform.position.x, vCamTarget.transform.position.y, vCamTarget.transform.position.z);
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
            transform.Translate(Vector3.left * vertMoveSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey("d"))
        {
            Debug.Log("d");
            transform.Translate(Vector3.left * -vertMoveSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey("w"))
        {
            Debug.Log("w");
            if (vCamTarget.transform.position.y < startRotation.y + 2f)
            {
                Debug.Log("looking up");
                vCamTarget.transform.Translate(Vector3.up * vertMoveSpeed * Time.deltaTime, Space.Self); ;
                vCamTarget.transform.Translate(Vector3.forward * -horizMoveSpeed * Time.deltaTime, Space.Self); ;
                //capsuleChild.transform.Rotate(transform.rotation.x + xRotationSpeed, transform.rotation.y, transform.rotation.z, Space.World);
            }
        }

        if (Input.GetKey("s"))
        {
            if (vCamTarget.transform.position.y > startRotation.y - .5f)
            {
                Debug.Log("looking down");

                vCamTarget.transform.Translate(Vector3.up * -vertMoveSpeed * Time.deltaTime, Space.Self); ;
                vCamTarget.transform.Translate(Vector3.forward * horizMoveSpeed * Time.deltaTime, Space.Self); ;
                //capsuleChild.transform.Rotate(transform.rotation.x - xRotationSpeed, transform.rotation.y, transform.rotation.z, Space.World);
            }
        }
    }
}
