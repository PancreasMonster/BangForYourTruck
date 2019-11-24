using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeMovement : MonoBehaviour
{

    public float suspensionForce;
    public float suspensionLenght;
    public float dampingForce;
    public List<Transform> hoverPoints = new List<Transform>();
    public Transform centreOfMass;
    RaycastHit flHit, frHit, blHit, brHit;
    Rigidbody rb;
    public LayerMask layer;

    private float springVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centreOfMass.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HoverForce();
    }

    void HoverForce ()
    {
        RaycastHit hit;
        foreach (Transform t in hoverPoints)
        {
            if (Physics.Raycast(t.position, -transform.up, out hit, suspensionLenght, layer))
            {
                rb.AddForceAtPosition(Vector3.up * suspensionForce * (1.0f - (hit.distance / suspensionLenght)), t.position);
                Debug.DrawLine(t.position, hit.point, Color.blue);
            }
           /* else
            {
                if(transform.position.y > t.transform.position.y)
                {
                    rb.AddForceAtPosition(t.transform.up * suspensionForce, t.transform.position); 
                } else
                {
                    rb.AddForceAtPosition(t.transform.up * -suspensionForce, t.transform.position);
                }
            } */


        }
        
    }
}
