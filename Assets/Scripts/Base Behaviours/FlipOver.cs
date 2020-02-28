﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOver : MonoBehaviour
{
    RaycastHit hit, hit2, hit3, hit4;
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
    bool delay;
    public bool turnDelay;
    public bool fakeGravity;
    public bool camDependent;
    public bool XtoRoll;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        h = GetComponentInParent<Health>();
    }

    private void Update()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), -transform.up, Color.red);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        {
            if (!delay)
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down, out hit2, 7.5f, layer))
                {
                    if (Input.GetButtonDown("PadA" + h.playerNum.ToString()))
                    {
                        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.up, out hit3, 7.5f, layer))
                        {
                            StartCoroutine(JumpDelay());
                            rigidbody.AddForce(-transform.up * force);
                            rigidbody.angularVelocity = Vector3.zero;
                        }
                        else
                        {
                            StartCoroutine(JumpDelay());
                          
                            rigidbody.AddForce(Vector3.up * force);
                            rigidbody.angularVelocity = Vector3.zero;
                        }
                        
                      //  Debug.Log("Hit");
                    }
                } else if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), -transform.up, out hit4, 7.5f, layer)) {
                    if (Input.GetButtonDown("PadA" + h.playerNum.ToString()))
                    {
                        StartCoroutine(JumpDelay());
                        rigidbody.AddForce(transform.up * force);
                        rigidbody.angularVelocity = Vector3.zero;

                        //  Debug.Log("Hit");
                    }
                }
        }

        if (fakeGravity)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), -transform.up, out hit, 10, layer) && !turnDelay)
            {
                timer = 0;
                rigidbody.AddForce(75f * -transform.up, ForceMode.Acceleration);
            }
            else
            {
                timer += Time.deltaTime;
                rigidbody.AddForce(75f * -Vector3.up, ForceMode.Acceleration);
            }
        } else
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), -transform.up, out hit, 10, layer) && !turnDelay)
            {
                timer = 0;
                
            }
            else
            {
                timer += Time.deltaTime;
               
            }
        }

        

        if (timer > timerAllowance)
        {
            

            if (XtoRoll)
            {
                float horAngle = Input.GetAxisRaw("Horizontal" + h.playerNum.ToString());
                float vertAngle = Input.GetAxisRaw("Vertical" + h.playerNum.ToString());
                if (Input.GetAxisRaw("Horizontal" + h.playerNum.ToString()) == 0 && Input.GetAxisRaw("Vertical" + h.playerNum.ToString()) == 0)
                    rigidbody.angularVelocity = rigidbody.angularVelocity * angularDamping;
                if (camDependent)
                {

                    rigidbody.AddTorque(cam.transform.right * vertAngle * angForce, ForceMode.Force);
                    if (Input.GetButton("PadX" + GetComponent<Health>().playerNum.ToString())) //this if statement reduces the steering angle when the vehicle approachs max speed and the drift button hasn't been used
                    {
                        rigidbody.AddTorque(cam.transform.forward * .75f * -horAngle * angForce, ForceMode.Force);
                    }
                    else
                    {
                        rigidbody.AddTorque(cam.transform.up * horAngle * angForce, ForceMode.Force);

                    }
                }
                else
                {
                    rigidbody.AddTorque(transform.right * vertAngle * angForce, ForceMode.Force);
                    if (Input.GetButton("PadX" + GetComponent<Health>().playerNum.ToString())) //this if statement reduces the steering angle when the vehicle approachs max speed and the drift button hasn't been used
                    {
                        rigidbody.AddTorque(transform.forward * .75f * -horAngle * angForce, ForceMode.Force);
                    }
                    else
                    {
                        rigidbody.AddTorque(transform.up * horAngle * angForce, ForceMode.Force);

                    }
                }
            }
            else
            {
               

                float horAngle = Input.GetAxisRaw("Horizontal" + h.playerNum.ToString());
                float vertAngle = Input.GetAxisRaw("Vertical" + h.playerNum.ToString());
                float rollAngle = Input.GetAxisRaw("RightTrigger" + h.playerNum.ToString()) - Input.GetAxisRaw("LeftTrigger" + h.playerNum.ToString());
                if (Input.GetAxisRaw("Horizontal" + h.playerNum.ToString()) == 0 && Input.GetAxisRaw("Vertical" + h.playerNum.ToString()) == 0)
                    rigidbody.angularVelocity = rigidbody.angularVelocity * angularDamping;
                if (camDependent)
                {
                    rigidbody.AddTorque(cam.transform.right * vertAngle * angForce, ForceMode.Force);
                    rigidbody.AddTorque(cam.transform.forward * rollAngle * angForce, ForceMode.Force);
                    rigidbody.AddTorque(cam.transform.up * horAngle * angForce, ForceMode.Force);

                }
                else
                {
                    rigidbody.AddTorque(transform.right * vertAngle * angForce, ForceMode.Force);
                    rigidbody.AddTorque(transform.forward * rollAngle * angForce, ForceMode.Force);
                    rigidbody.AddTorque(transform.up * horAngle * angForce, ForceMode.Force);
                }
            }
            
            
        }     

    }

    


    IEnumerator JumpDelay()
    {
        turnDelay = true;
        delay = true;
        timer = timerAllowance;        
        yield return null;
        rigidbody.angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(.25f);
        turnDelay = false;
        yield return new WaitForSeconds(1f);
        delay = false;
    }
}
