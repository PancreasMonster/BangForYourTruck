using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinStick : MonoBehaviour
{
    public bool LeftStickMovement;
    public float rotationSpeed = 1.5f;
    public float force;
    Rigidbody rb;
    float mouseX, mouseY;
    RaycastHit hit;
    public LayerMask layer;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 20, layer))
        {
            //transform.right * Input.GetAxisRaw("Horizontal" + GetComponent<Health>().playerNum.ToString()) + 
            Vector3 playerMovement = transform.forward * Input.GetAxis("Vertical" + GetComponent<Health>().playerNum.ToString());
            //if (playerMovement.sqrMagnitude > 0.0f)
            //  {
            rb.AddForce(playerMovement * force);
            //  }


            /* Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal" + GetComponent<Health>().playerNum.ToString()); // + transform.forward * -Input.GetAxisRaw("RVertical" + GetComponent<Health>().playerNum.ToString());
             if(playerDirection.sqrMagnitude > 0.0f)
             {
                 transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
             } */
        } else
        {
            Vector3 playerMovement = cam.transform.forward * Input.GetAxis("Vertical" + GetComponent<Health>().playerNum.ToString());
            rb.AddForce(playerMovement * force);
        }
    }

    void LateUpdate()
    {
        CamControl();
    }

    void CamControl ()
    {
        if (Input.GetAxis("RHorizontal" + GetComponent<Health>().playerNum.ToString()) != 0) { 
        //mouseY -= Input.GetAxis("RVertical" + GetComponent<Health>().playerNum.ToString()) * rotationSpeed;
        // mouseY = Mathf.Clamp(mouseY, -35, 60);
        mouseX = Input.GetAxis("RHorizontal" + GetComponent<Health>().playerNum.ToString());
        rb.AddTorque(Vector3.up * mouseX * rotationSpeed, ForceMode.VelocityChange);
            } else
        {
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
