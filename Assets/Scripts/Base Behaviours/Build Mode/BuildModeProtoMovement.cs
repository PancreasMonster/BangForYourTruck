using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeProtoMovement : MonoBehaviour
{
    public float adjustSpeed = 1;
    Quaternion fromRotation;
    Quaternion toRotation;
    Vector3 targetNormal;
    RaycastHit hit;
    float weight = 9;
    public LayerMask layer;

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
       
            if (Physics.Raycast(transform.position, -transform.up, out hit, 20, layer))
            {

                targetNormal = hit.normal;
                fromRotation = transform.rotation;
                toRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                weight = 0;


                if (weight <= 1)
                {
                    weight += Time.deltaTime * adjustSpeed;
                    transform.rotation = Quaternion.Slerp(fromRotation, toRotation, weight);

                }
            }
       
    }


}
