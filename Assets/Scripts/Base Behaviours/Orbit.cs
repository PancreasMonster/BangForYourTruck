﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Orbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player, playerPos, hoverBox, carDeath;
    public int playerNum;
    public float timeAllowance;
    public LayerMask layer;
    public List<Transform> wheelVisuals = new List<Transform>();
    private RaycastHit hit;
    private float timer;
    public Vector3 offset = new Vector3(0, 10, -26);
    private Vector3 origPos;
    private Vector3 jumpOffset;
    private bool lockedBehind = true;
    private bool disorient;
    public FlipOver fo;
    public float setDrag = 3;
    public float animSpeed;
    private WheelCollider[] wheels;
    public bool death;
    public float lookOffsetY = 3;
    public float yLookAmount;
    public AudioSource aud;
    public LockOn lockOnScript;
    public GameObject lockOnParent;
    public Transform cam;
    public float lerpSpeed = 30;
    public float rotateSpeed = 180;
    float rotateAmount;
    float yRotateAmount;
    float yAirRotation;
    float wallTransition;

    void Start()
    {
        //offset = new Vector3(0, 10, -26);
        origPos = offset;
        wheels = player.GetComponentsInChildren<WheelCollider>();
        lockOnScript = player.GetComponent<LockOn>();
    }

    Vector2 rightStick;
    float leftBumper;

    private void OnRightStickClick(InputValue value)
    {

        /*lockedBehind = !lockedBehind;
        offset = origPos;
        offset = Quaternion.AngleAxis(player.transform.localEulerAngles.y - 180, Vector3.up) * offset;
        */
        origPos = new Vector3(origPos.x, origPos.y, -origPos.z);
        jumpOffset = new Vector3(jumpOffset.x, jumpOffset.y, -jumpOffset.z);
    }

    private void OnRightStickRelease(InputValue value)
    {

        /*lockedBehind = !lockedBehind;
        offset = origPos;
        offset = Quaternion.AngleAxis(player.transform.localEulerAngles.y - 180, Vector3.up) * offset;
        */
        origPos = new Vector3(origPos.x, origPos.y, -origPos.z);
        jumpOffset = new Vector3(jumpOffset.x, jumpOffset.y, -jumpOffset.z);
    }

    private void OnRightStick(InputValue value)
    {

        rightStick = value.Get<Vector2>();
        
    }

    private void OnLeftBumper(InputValue value)
    {

        leftBumper = 1;

    }

    private void OnLeftBumperRelease(InputValue value)
    {

        leftBumper = 0;

    }

    /* private void Update()
     {
         if (fo.timer < timeAllowance)
         {
             if (Input.GetButtonDown("PadLB" + playerNum.ToString()) && !death)
             {
                 Rigidbody rb = player.GetComponent<Rigidbody>();
                 rb.AddForce(Vector3.up * 22500);
                 rb.drag = setDrag;
                 rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                 player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0);
                 if(aud != null)
                 aud.Stop();
                 player.GetComponent<RearWheelDrive>().enabled = false;
                 player.GetComponent<BuildModeProtoMovement>().enabled = true;
                 player.GetComponent<BuildModeFire>().enabled = true;


                 foreach (WheelCollider w in wheels)
                 {
                     w.gameObject.SetActive(false);
                 }
                 player.GetComponent<LineRenderer>().enabled = true;
                 player.GetComponent<FireDisk>().enabled = false;
                 hoverBox.GetComponent<BoxCollider>().enabled = true;
                 GetComponent<BuildModeCamera>().changeToThis(wheels);
                 GetComponent<BuildModeCamera>().enabled = true;
                 GetComponent<BuildModeCamera>().ToggleUIElements();
                 GetComponent<Orbit>().enabled = false;
             } 
         } 
     } */



    void FixedUpdate()
    {
        if (lockOnScript.target == null)
        {
            if (!death)
            {
                rotateAmount = Mathf.Lerp(rotateAmount, 90 * rightStick.x, rotateSpeed * Time.deltaTime);
                if(rightStick.y > 0)
                yRotateAmount = Mathf.Lerp(yRotateAmount, 65 * rightStick.y, rotateSpeed * Time.deltaTime);
                if (rightStick.y < 0)
                    yRotateAmount = Mathf.Lerp(yRotateAmount, 22.5f * rightStick.y, rotateSpeed * Time.deltaTime);
                if (rightStick.y == 0)
                    yRotateAmount = Mathf.Lerp(yRotateAmount, 0, rotateSpeed * Time.deltaTime);
                Vector3 dir = player.position - cam.transform.position;
                dir.Normalize();
                Vector3 tempOffset = origPos;
                //tempOffset = Quaternion.AngleAxis(player.transform.localEulerAngles.y - 180, Vector3.up) * tempOffset;
                Vector3 wallOffset = fo.hit.normal;
                Vector3 wallOffsetY = fo.hit.normal;
                
                if (Vector3.Dot(Vector3.forward, fo.hit.normal.normalized) > .8f || Vector3.Dot(Vector3.forward, fo.hit.normal.normalized) < -.8f)
                {
                    wallOffset = Quaternion.AngleAxis(-90, Vector3.up) * wallOffset;
                    
                }

                if (Vector3.Dot(Vector3.up, fo.hit.normal) < -.8f || Vector3.Dot(Vector3.up, fo.hit.normal) > .8f)
                {
                    wallOffsetY = Quaternion.AngleAxis(90, Vector3.forward) * wallOffsetY;
                }

                    if (Vector3.Dot(Vector3.up, fo.hit.normal) < -.8f)
                {

                    wallTransition = Mathf.Lerp(wallTransition, -180, lerpSpeed * .3f * Time.deltaTime);
                    tempOffset = new Vector3(tempOffset.x, -tempOffset.y, tempOffset.z);
                } else
                {
                    wallTransition = Mathf.Lerp(wallTransition, 0, lerpSpeed * .3f * Time.deltaTime);
                }
                    tempOffset = Quaternion.AngleAxis(-rotateAmount, wallOffset) * tempOffset;
                    tempOffset = Quaternion.AngleAxis(-yRotateAmount, wallOffsetY) * tempOffset;
                Vector3 appliedOffset = tempOffset;

                if (lockedBehind)
                {

                    
                    if (fo.timer > timeAllowance)
                    {
                        if (!disorient)
                        {
                             jumpOffset = origPos;                        
                            jumpOffset = Quaternion.AngleAxis(player.eulerAngles.y, Vector3.up) * jumpOffset;
                            
                            disorient = true;
                        }
                       
                        Vector3 jumpTempOffset = jumpOffset;
                        jumpTempOffset = Quaternion.AngleAxis(-rotateAmount, Vector3.up) * jumpTempOffset;
                        jumpTempOffset = Quaternion.AngleAxis(yRotateAmount, Vector3.right) * jumpTempOffset;
                        Vector3 jumpAppliedOffset = jumpTempOffset;
                        cam.transform.position = Vector3.Lerp(cam.transform.position, playerPos.position + jumpAppliedOffset, lerpSpeed * 1 * Time.deltaTime);
                        if (Vector3.Dot(Vector3.up, fo.hit.normal) < -.8f)
                        {
                            cam.transform.LookAt(new Vector3(playerPos.position.x, playerPos.position.y + yLookAmount, playerPos.position.z));
                        }
                        else
                        {
                            cam.transform.LookAt(new Vector3(playerPos.position.x, playerPos.position.y + yLookAmount, playerPos.position.z));
                        }
                    }
                    else
                    {

                        //if (leftBumper == 0)
                            cam.transform.position = Vector3.Lerp(cam.transform.position,
                                new Vector3(playerPos.TransformPoint(appliedOffset).x, playerPos.TransformPoint(appliedOffset).y, playerPos.TransformPoint(appliedOffset).z),
                                //player.transform.position + appliedOffset,
                                lerpSpeed * Time.deltaTime);
                      //  else
                      //      cam.transform.position = Vector3.Lerp(cam.transform.position,
                      //          new Vector3(playerPos.TransformPoint(appliedOffset).x, playerPos.TransformPoint(appliedOffset).y, playerPos.TransformPoint(appliedOffset).z),
                      //          lerpSpeed * Time.deltaTime);

                       

                        
                        if (Vector3.Dot(Vector3.up, fo.hit.normal) < -.8f)
                        {
                            cam.transform.LookAt(playerPos.TransformPoint(new Vector3(0, -yLookAmount, 0)));
                            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, wallTransition);
                        } else
                        {
                            cam.transform.LookAt(playerPos.TransformPoint(new Vector3(0, yLookAmount, 0)));
                        }
                        disorient = false;
                    }

                    if(!disorient)
                        
                    if (Vector3.Dot(Vector3.up, fo.hit.normal) < -.8f)
                    {
                            cam.transform.LookAt(playerPos.TransformPoint(new Vector3(0, -yLookAmount, 0)));
                            
                        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, wallTransition);
                    } else
                    {
                            cam.transform.LookAt(playerPos.TransformPoint(new Vector3(0, yLookAmount, 0)));
                            
                        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, wallTransition);
                    }
                }

                if (!lockedBehind)
                {
                    cam.transform.position = playerPos.position + offset;
                    offset = Quaternion.AngleAxis(rightStick.x * turnSpeed * Time.deltaTime, Vector3.up) * offset;
                    if (leftBumper == 0)
                        cam.transform.LookAt(playerPos.TransformPoint(new Vector3(0, 0 + (rightStick.y * -yLookAmount), 0)));
                    else
                        cam.transform.LookAt(playerPos.TransformPoint(new Vector3(0, 0, 0)));
                }
            }
            else
            {
                if (carDeath != null)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, carDeath.position + offset, lerpSpeed * Time.deltaTime);
                    cam.transform.LookAt(carDeath.position);
                }
                else
                {

                }
            }
        } else
        {
            if (!death)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, lockOnParent.transform.position, lerpSpeed * 2f * Time.deltaTime);
                cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, lockOnParent.transform.rotation, lerpSpeed * 2f * Time.deltaTime);
            }
            else
            {
                if (carDeath != null)
                {
                    cam.transform.position = carDeath.position + offset;
                    cam.transform.LookAt(carDeath.position);
                }
                else
                {

                }
            }
        }

    
    }
}
