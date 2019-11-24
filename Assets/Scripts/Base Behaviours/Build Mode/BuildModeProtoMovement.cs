using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeProtoMovement : MonoBehaviour
{
    public float turnSpeed;
    public float moveSpeed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("RHorizontal" + GetComponent<Health>().playerNum.ToString()) * turnSpeed * Time.deltaTime);
    }

     void FixedUpdate()
    {
        float moveZ = Input.GetAxisRaw("Vertical" + GetComponent<Health>().playerNum.ToString());
        float moveX = Input.GetAxisRaw("Horizontal" + GetComponent<Health>().playerNum.ToString());
        rb.AddForce((transform.forward * moveZ * moveSpeed * Time.deltaTime) + (transform.right * moveX * moveSpeed * Time.deltaTime));
        
    }


}
