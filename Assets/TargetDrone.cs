using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDrone : MonoBehaviour
{
    public DroneWaypoints dwp;

    public Transform player;

    public Vector3 nextWaypoint;

    Rigidbody rb;

    public float arriveDistance;

    public float droneSpeed;

    public float rotationSpeed = 5;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        nextWaypoint = dwp.NextWaypoint();

        {
            if (Vector3.Distance(transform.position, nextWaypoint) < arriveDistance)
            {
                Vector3 lookDir = nextWaypoint - rb.position;

                lookDir.Normalize();

                Vector3 rotateAmount = Vector3.Cross(transform.forward, lookDir);

                Vector3 spinRotateAmount = Vector3.Cross(transform.up, Vector3.up);

                rb.angularVelocity = (rotateAmount + spinRotateAmount) * rotationSpeed;



                Vector3 dir = nextWaypoint - rb.position;
                dir.Normalize();

                rb.AddForce(dir * droneSpeed * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }

    public void AdvanceToNextWaypoint()
    {
        dwp.AdvanceToNext();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "TrainingDroneWaypoint")
        {
            AdvanceToNextWaypoint();
        }
    }
}
