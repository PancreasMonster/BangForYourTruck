using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOver : MonoBehaviour
{
    RaycastHit hit, hit2;
    public LayerMask layer;
    public float force, angForce;
    [Range(0,1)]
    public float angularDamping; //how much of a percentage of the previous frame's angular velocity is carried to the next frame
    Rigidbody rigidbody;
    Health h;
    Quaternion toRotation;
    Quaternion fromRotation;
    Vector3 targetNormal;
    Vector3 T;
    bool Flip;
    bool cooldown;
    public float timer;
    public Camera cam;
    public float timerAllowance = .4f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        h = GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (!Flip)
        {
            timer += Time.deltaTime;
        } 
      

        if (timer > timerAllowance)
        {
            float horAngle = Input.GetAxisRaw("Horizontal" + h.playerNum.ToString());
            float vertAngle = Input.GetAxisRaw("Vertical" + h.playerNum.ToString());
            if (Input.GetAxisRaw("Horizontal" + h.playerNum.ToString()) == 0 && Input.GetAxisRaw("Vertical" + h.playerNum.ToString()) == 0)
                rigidbody.angularVelocity = rigidbody.angularVelocity * angularDamping;
            rigidbody.AddTorque(cam.transform.forward * -horAngle * angForce);
            rigidbody.AddTorque(cam.transform.right * vertAngle * angForce);
        }     

    }

    private void OnTriggerEnter(Collider other)
    {
        Flip = true;
        timer = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        Flip = true;
        timer = 0;
    }

    private void OnTriggerExit(Collider other)
    {
        Flip = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -Vector3.up * 1f);
    }
}
