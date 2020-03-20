using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    public DroneWaypoints dwp;

    public List<Transform> players = new List<Transform>();

    Vector3 nextWaypoint;

    public float arriveDistance;

    public float droneSpeed;

    public float droneCheckDistance;

    public float rotationSpeed = 5;

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
        
    }

    public void FixedUpdate()
    {
        nextWaypoint = dwp.NextWaypoint();
        if (Vector3.Distance(transform.position, nextWaypoint) < arriveDistance)
        {
            dwp.AdvanceToNext();
        }

        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, droneSpeed * Time.deltaTime);

        float maxDistance = droneCheckDistance;
        int index = -1;
        for(int i = 0; i < players.Count; i++)
        {
            if (Vector3.Distance(players[i].position, transform.position) < maxDistance)
            {
                index = i;
                
                maxDistance = Vector3.Distance(players[i].position, transform.position);
            }
        }


        if (index > -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(players[index].position - transform.position), rotationSpeed * Time.deltaTime);
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextWaypoint - transform.position), rotationSpeed * Time.deltaTime);
        }
    }

}
