using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitalLaserSpawn : MonoBehaviour
{
    bool laserSpawned = false;

    public float laserRechargeTime;

    public GameObject orbitalLaser, currentOrbitalLaser;

    public float zPosBehind;

    public float laserHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float DPADUpDown;

    private void OnDPADUpDown(InputValue value)
    {
        DPADUpDown = value.Get<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DPADUpDown > 0 && !laserSpawned)
        {
            laserSpawned = true;
            GameObject clone = Instantiate(orbitalLaser, new Vector3(transform.position.x, transform.position.y + laserHeight, transform.position.z + zPosBehind), orbitalLaser.transform.rotation);
            currentOrbitalLaser = clone;
            currentOrbitalLaser.GetComponent<OrbitalLaser>().laserWaypoints.Add(currentOrbitalLaser.transform.position);
            currentOrbitalLaser.GetComponent<OrbitalLaser>().followPlayer = transform;
            currentOrbitalLaser.GetComponent<OrbitalLaser>().teamNum = GetComponent<Health>().teamNum;
            StartCoroutine(CreateLaserWaypoint());
            StartCoroutine(RechargeLaser());
        }


    }

    IEnumerator CreateLaserWaypoint()
    {
        yield return new WaitForSeconds(1);
        if (currentOrbitalLaser)
        {
            currentOrbitalLaser.GetComponent<OrbitalLaser>().laserWaypoints.Add(new Vector3(transform.position.x, transform.position.y + laserHeight, transform.position.z));
            StartCoroutine(CreateLaserWaypoint());
        }
    }

    IEnumerator RechargeLaser()
    {
        yield return new WaitForSeconds(laserRechargeTime);
        laserSpawned = false;
    }
}
