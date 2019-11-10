using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOver : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask layer;
    public float force, angForce;
    Rigidbody rigidbody;
    Vector3 T;
    bool Flip;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.up, out hit, 10, layer))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * force);
                GetComponent<Rigidbody>().AddTorque(Vector3.right * angForce);
            }
        }



        if (Vector3.Dot(transform.up, Vector3.up) < .2f)
        {
            if (!Flip)
                Flip = true;
            Vector3 x = Vector3.Cross(transform.position.normalized, Vector3.up.normalized);
            float theta = Mathf.Asin(x.magnitude);
            Vector3 w = x.normalized * theta / Time.fixedDeltaTime;

            Quaternion q = transform.rotation * rigidbody.inertiaTensorRotation;
            T = q * Vector3.Scale(rigidbody.inertiaTensor, (Quaternion.Inverse(q) * w));

            rigidbody.AddTorque(T * 1f);
        } else if (Flip) {
            Flip = false;
            rigidbody.angularVelocity = rigidbody.angularVelocity * .2f;
        }
        Debug.Log((Vector3.Dot(transform.up, Vector3.up)));
    }

   
}
