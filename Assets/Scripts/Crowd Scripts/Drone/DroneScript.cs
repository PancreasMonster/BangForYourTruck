using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    public DroneWaypoints dwp;

    public List<Transform> players = new List<Transform>();

    Vector3 nextWaypoint;

    Rigidbody rb;

    public float arriveDistance;

    public float followDistance = 100;

    public float backOffDistance;

    public float droneSpeed;

    public float droneCheckDistance;

    public float rotationSpeed = 5;

    public float heightAbovePlayer;

    public float riseSpeed;

    public Collider droneCol;

    bool onlyFollowWaypoints;

    public float stateTime;

    [FMODUnity.EventRef]
    public string closeUpDialogue;

    bool sayCloseUpDialogue = true;

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
        StartCoroutine(OnlyFollowWayPoints());
    }

    public void FixedUpdate()
    {
        nextWaypoint = dwp.NextWaypoint();
        

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


        if (index > -1 && droneCol.bounds.Contains(players[index].position) && !onlyFollowWaypoints)
        {
            if(sayCloseUpDialogue)
            {
                sayCloseUpDialogue = false;
                FMODUnity.RuntimeManager.PlayOneShot(closeUpDialogue);
            }

            Vector3 lookDir = players[index].position - rb.position;

            lookDir.Normalize();

            Vector3 spinRotateAmount = Vector3.Cross(transform.up, Vector3.up);

            Vector3 rotateAmount = Vector3.Cross(transform.forward, lookDir);


            rb.angularVelocity = (rotateAmount + spinRotateAmount) * rotationSpeed;
            

            Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);

            Vector3 dir = players[index].position - transform.position;
            dir.Normalize();

            if (maxDistance > followDistance)
                rb.AddForce(dir * droneSpeed * Time.deltaTime, ForceMode.VelocityChange);
            else if(maxDistance < backOffDistance)
            {
                rb.AddForce(-dir * droneSpeed * Time.deltaTime, ForceMode.VelocityChange);
            }

            if((transform.position.y - players[index].position.y) < heightAbovePlayer)
            {
                rb.AddForce(Vector3.up * riseSpeed * (heightAbovePlayer/Mathf.Abs(transform.position.y - players[index].position.y)) * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        else
        {
            sayCloseUpDialogue = true;

            Vector3 lookDir = nextWaypoint - rb.position;

            lookDir.Normalize();

            Vector3 rotateAmount = Vector3.Cross(transform.forward, lookDir);

            Vector3 spinRotateAmount = Vector3.Cross(transform.up, Vector3.up);

            rb.angularVelocity = rotateAmount * rotationSpeed;
            rb.angularVelocity = spinRotateAmount * rotationSpeed;

            if (Vector3.Distance(transform.position, nextWaypoint) < arriveDistance)
            {
                dwp.AdvanceToNext();
            }

            Vector3 dir = nextWaypoint - transform.position;
            dir.Normalize();

            rb.AddForce(dir * droneSpeed * Time.deltaTime, ForceMode.VelocityChange);

        }

       
    }

    IEnumerator OnlyFollowWayPoints ()
    {
        onlyFollowWaypoints = true;
        yield return new WaitForSeconds(stateTime);
        StartCoroutine(AllowPlayerFollow());
    }

    IEnumerator AllowPlayerFollow ()
    {
        onlyFollowWaypoints = false;
        yield return new WaitForSeconds(stateTime);
        StartCoroutine(OnlyFollowWayPoints());
    }

}
