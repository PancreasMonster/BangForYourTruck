using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinStick : MonoBehaviour
{
    public bool LeftStickMovement;
    public float rotationSpeed = 2;
    public float force;
    Rigidbody rb;
    float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
        
    }

    void LateUpdate()
    {
        CamControl();
    }

    void CamControl ()
    {
        mouseX += Input.GetAxis("RHorizontal" + GetComponent<Health>().playerNum.ToString()) * rotationSpeed;
        //mouseY -= Input.GetAxis("RVertical" + GetComponent<Health>().playerNum.ToString()) * rotationSpeed;
       // mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
