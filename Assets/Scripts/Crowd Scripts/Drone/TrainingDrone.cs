using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDrone : MonoBehaviour
{
    public DroneWaypoints dwp;

    public Transform player;

    public Vector3 nextWaypoint;

    Rigidbody rb;

    public float arriveDistance;

    public float droneSpeed;

    public float rotationSpeed = 5;

    public TrainingManager tm;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, nextWaypoint);
        }
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        nextWaypoint = dwp.NextWaypoint();


        if (Vector3.Distance(transform.position, nextWaypoint) < arriveDistance)
        {
            Vector3 lookDir = player.position - rb.position;

            lookDir.Normalize();

            Vector3 spinRotateAmount = Vector3.Cross(transform.up, Vector3.up);

            Vector3 rotateAmount = Vector3.Cross(transform.forward, lookDir);

            rb.angularVelocity = (rotateAmount + spinRotateAmount) * rotationSpeed;
        }
        else
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

    public void AdvanceToNextWaypoint()
    {
        dwp.AdvanceToNext();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "TrainingDroneWaypoint")
        {

            if (other.transform.name == "Waypoint (3)")
            {
                tm.ProceedToTraining1();
            }
            else
            {
                AdvanceToNextWaypoint();
            }
            
        }

        
    }

}
