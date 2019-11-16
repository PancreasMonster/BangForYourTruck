using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOver : MonoBehaviour
{
    RaycastHit hit, hit2;
    public LayerMask layer;
    public float force, angForce;
    Rigidbody rigidbody;
    Quaternion toRotation;
    Quaternion fromRotation;
    Vector3 targetNormal;
    Vector3 T;
    bool Flip;
    bool cooldown;
    float timer;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, Vector3.down * 5, out hit, 5, layer))
        {
            if (Input.GetButtonDown("RightStick" + GetComponent<Health>().playerNum.ToString()))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * force);
                //StartCoroutine(FlipBack());
            }
        }

        if (Physics.Raycast(transform.position, -transform.up, out hit2, 5, layer))
        {
            timer = 0;
        } else
        {
            timer += Time.deltaTime;
        }

        if (timer > .25f)
        {
            float horAngle = Input.GetAxisRaw("Horizontal" + GetComponent<Health>().playerNum.ToString());
            float vertAngle = Input.GetAxisRaw("Vertical" + GetComponent<Health>().playerNum.ToString());
            rigidbody.AddTorque(cam.transform.forward * -horAngle * angForce);
            rigidbody.AddTorque(cam.transform.right * vertAngle * angForce);
        }

        /* if (Vector3.Dot(transform.up, Vector3.up) < .2f)
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
         Debug.Log((Vector3.Dot(transform.up, Vector3.up)));*/

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -Vector3.up * .75f);
    }

    public IEnumerator FlipBack()
    {
        cooldown = true;
        //yield return new WaitForSeconds(.25f);
       /* if (Physics.Raycast(transform.position, -Vector3.up, out hit, 20, layer))
        {

            targetNormal = hit.normal;
            fromRotation = transform.rotation;
            toRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, 1f * Time.deltaTime);
        } */
        yield return new WaitForSeconds(1);
        cooldown = false;
        
    }
}
