using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinStick : MonoBehaviour
{
    public bool LeftStickMovement;
    public float force;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftStickMovement) {
            Vector3 playerMovement = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");
            if (playerMovement.sqrMagnitude > 0.0f)
            {
                rb.AddForce(playerMovement * force);
            }
        }

        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
        if(playerDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        }
    }
}
