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

    public bool killM;

    public KillManager km;

    [FMODUnity.EventRef]
    public string closeUpDialogue;

    [FMODUnity.EventRef]
    public string deathDialogue;

    [FMODUnity.EventRef]
    public string whatHappenedDialogue;

    [FMODUnity.EventRef]
    public string respawnDialogue;

    bool sayCloseUpDialogue = true;

    bool dead = false;

    bool checkForCorpse = false;

    Vector3 spawnPoint;

    Camera cam;

    public GameObject droneDeathPrefab;

    Transform droneCorpse;

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
        spawnPoint = transform.position;
        cam = GetComponentInChildren<Camera>();
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

            rb.angularVelocity = (rotateAmount + spinRotateAmount) * rotationSpeed;

            if (Vector3.Distance(transform.position, nextWaypoint) < arriveDistance)
            {
                dwp.AdvanceToNext();
            }

            Vector3 dir = nextWaypoint - transform.position;
            dir.Normalize();

            rb.AddForce(dir * droneSpeed * Time.deltaTime, ForceMode.VelocityChange);

        }

        if(!dead && checkForCorpse)
        {
            if(Vector3.Distance(transform.position, droneCorpse.position) < droneCheckDistance / 4)
            {
                FMODUnity.RuntimeManager.PlayOneShot(whatHappenedDialogue);
                checkForCorpse = false;
            }
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

    public void DeathTrigger (int playerNum)
    {
        StartCoroutine(DroneDeath());
        km.droneKills[playerNum-1]++;
    }

    IEnumerator DroneDeath()
    {
        if(droneCorpse)
        {
            droneCorpse.GetComponent<DroneDeath>().StopAllCoroutines();
            Destroy(droneCorpse);
            droneCorpse = null;
        }
        dead = true;
        cam.enabled = false;
        FMODUnity.RuntimeManager.PlayOneShot(deathDialogue);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        GetComponent<BoxCollider>().enabled = false;
        GameObject droneDeathBody = Instantiate(droneDeathPrefab, transform.position, droneDeathPrefab.transform.rotation);
        droneCorpse = droneDeathBody.transform;
        yield return new WaitForSeconds(30);
        rb.position = spawnPoint;
        FMODUnity.RuntimeManager.PlayOneShot(respawnDialogue);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Health>().health = GetComponent<Health>().maxHealth;
        dead = false;
        cam.enabled = true;
        checkForCorpse = true;
    }
}
