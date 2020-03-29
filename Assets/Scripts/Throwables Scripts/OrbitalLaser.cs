using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalLaser : MonoBehaviour
{
   
    public float laserSpeed;

    public float laserLifeTime;

    public float laserStartTime;

    bool laserActivate;

    Vector3 nextWaypoint;

    Rigidbody rb;

    public float arriveDistance;

    public List<Vector3> laserWaypoints = new List<Vector3>();

    public int next = 0;
    public bool looped = true;

    public Transform followPlayer;

    public int teamNum;

    public void OnDrawGizmos()
    {
        int count = looped ? (transform.childCount + 1) : transform.childCount;
        Gizmos.color = Color.cyan;
        for (int i = 1; i < count; i++)
        {
            Transform prev = transform.GetChild(i - 1);
            Transform next = transform.GetChild(i % transform.childCount);
            Gizmos.DrawLine(prev.transform.position, next.transform.position);
            Gizmos.DrawSphere(prev.position, 1);
            Gizmos.DrawSphere(next.position, 1);
        }
    }

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, laserLifeTime);
        StartCoroutine(laserActivation());
    }

    // Update is called once per frame
    void Update()
    {
        if (laserActivate)
        {

            Vector3 dir = followPlayer.position - transform.position;
            dir.Normalize();
            transform.Translate(dir * laserSpeed * Time.deltaTime);

        }
    }

    public Vector3 NextWaypoint()
    {
        return laserWaypoints[next];
    }

    public void AdvanceToNext()
    {
        if (looped)
        {
            next = (next + 1) % laserWaypoints.Count;
        }
        else
        {
            if (next != laserWaypoints.Count - 1)
            {
                next++;
            }
        }
    }

    IEnumerator laserActivation ()
    {
        yield return new WaitForSeconds(laserStartTime);
        laserActivate = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Health>() != null && col.gameObject != this.gameObject)
        {
            if (col.gameObject.GetComponent<Health>().teamNum != teamNum)
            {
                col.gameObject.GetComponent<Health>().TakeDamage("Lasered", followPlayer.gameObject, 100, Vector3.zero);
            }
        }
    }
}
