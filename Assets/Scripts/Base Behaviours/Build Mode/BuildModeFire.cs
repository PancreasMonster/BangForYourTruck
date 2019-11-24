using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeFire : MonoBehaviour
{
    LineRenderer lr;

    public float velocity;
    public float fireAngle;
    public float mortarSpeed;
    public Transform aimTarget, firingPoint;
    [Range (1, 50)]
    public int resolution;

    float g;
    float radianAngle;

     void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderArc();
        FindVelocity(aimTarget, fireAngle);
    }

    // Renders the trajectory path of projectile
    void RenderArc ()
    {
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArc());
    }

    Vector3[] CalculateArc()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * fireAngle;
        float maxDistance = (velocity * velocity * Mathf.Sin (2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    //calculates each point of the array 
    Vector3 CalculateArcPoint (float t, float maxDistance)
    {
        float z = t * maxDistance;
        float y = z * Mathf.Tan(radianAngle) - ((g * z * z) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        Vector3 arcRot = new Vector3(0, y, z);
        transform.TransformPoint(arcRot);
        return arcRot;
    }

    public Vector3 BallisticVel(Transform target, float angle)
    {
        Vector3 targetDir = target.position - firingPoint.position;
        float dist = targetDir.magnitude;
        float radAngle = angle * Mathf.Deg2Rad;
        targetDir.y = dist * Mathf.Tan(radAngle);
        float fireVelocity = Mathf.Sqrt(dist * Physics.gravity.magnitude * mortarSpeed / Mathf.Sin(radAngle * 2));
        return fireVelocity * targetDir.normalized;
    }

    public void FindVelocity (Transform target, float angle)
    {
        Vector3 targetDir = target.position - firingPoint.position;
        float dist = targetDir.magnitude;
        float radAngle = angle * Mathf.Deg2Rad;
        targetDir.y = dist * Mathf.Tan(radAngle);
        velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude * mortarSpeed / Mathf.Sin(radAngle * 2));
    }
}
