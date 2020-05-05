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

    public GameObject droneCorpse;

    public GameObject droneDeathPrefab;

    bool dead = false;

    public TrainingManager tm;

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


    public void DeathTrigger()
    {
        StartCoroutine(DroneDeath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DroneDeath()
    {
        if (droneCorpse)
        {
            droneCorpse.GetComponent<DroneDeath>().StopAllCoroutines();
            Destroy(droneCorpse);
            droneCorpse = null;
        }
        dead = true;
        //cam.enabled = false;
        //FMODUnity.RuntimeManager.PlayOneShot(deathDialogue);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        GetComponent<BoxCollider>().enabled = false;
        GameObject droneDeathBody = Instantiate(droneDeathPrefab, transform.position, droneDeathPrefab.transform.rotation);
        yield return null;
        tm.DroneKilled();
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
