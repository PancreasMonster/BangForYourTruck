using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Orbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player, hoverBox, carDeath;
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
    }

    private void OnRightStickRelease(InputValue value)
    {

        /*lockedBehind = !lockedBehind;
        offset = origPos;
        offset = Quaternion.AngleAxis(player.transform.localEulerAngles.y - 180, Vector3.up) * offset;
        */
        origPos = new Vector3(origPos.x, origPos.y, -origPos.z);
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



    void Update()
    {
        if (lockOnScript.target == null)
        {
            if (!death)
            {
                Vector3 dir = player.position - cam.transform.position;
                dir.Normalize();
                Vector3 tempOffset = origPos;
                tempOffset = Quaternion.AngleAxis(rightStick.x * 90, Vector3.up) * tempOffset;
                Vector3 appliedOffset = tempOffset;

                if (lockedBehind)
                {

                    
                    if (fo.timer > timeAllowance)
                    {
                        if (!disorient)
                        {
                            jumpOffset = origPos;
                            jumpOffset = Quaternion.AngleAxis(player.transform.localEulerAngles.y - 180, Vector3.up) * jumpOffset;
                            
                            disorient = true;
                        }
                        Vector3 jumpTempOffset = jumpOffset;
                        jumpTempOffset = Quaternion.AngleAxis(rightStick.x * 90, Vector3.up) * jumpTempOffset;
                        Vector3 jumpAppliedOffset = jumpTempOffset;
                        cam.transform.position = Vector3.Lerp(cam.transform.position, player.position + jumpAppliedOffset, lerpSpeed * Time.deltaTime);
                        if (leftBumper == 0)
                            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(player.TransformPoint(new Vector3(0, 0 + (rightStick.y * -yLookAmount), 0)) - cam.transform.position), lerpSpeed * Time.deltaTime);
                        else
                            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(player.TransformPoint(new Vector3(0, 0, 0)) - cam.transform.position), lerpSpeed * Time.deltaTime);
                    }
                    else
                    {
                      
                        if (leftBumper == 0)
                            cam.transform.position = Vector3.Lerp(cam.transform.position, 
                                new Vector3(player.TransformPoint(appliedOffset).x, player.TransformPoint(appliedOffset).y + (rightStick.y * -yLookAmount), player.TransformPoint(appliedOffset).z),
                                lerpSpeed * Time.deltaTime);
                        else
                            cam.transform.position = Vector3.Lerp(cam.transform.position,
                                new Vector3(player.TransformPoint(appliedOffset).x, player.TransformPoint(appliedOffset).y, player.TransformPoint(appliedOffset).z),
                                lerpSpeed * Time.deltaTime);

                        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(player.TransformPoint(new Vector3(0, lookOffsetY, 0)) - cam.transform.position), lerpSpeed * Time.deltaTime);
                        disorient = false;
                    }

                    if(!disorient)
                        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(player.TransformPoint(new Vector3(0, lookOffsetY, 0)) - cam.transform.position), lerpSpeed * Time.deltaTime);
                }

                if (!lockedBehind)
                {
                    cam.transform.position = player.position + offset;
                    offset = Quaternion.AngleAxis(rightStick.x * turnSpeed * Time.deltaTime, Vector3.up) * offset;
                    if (leftBumper == 0)
                        cam.transform.LookAt(player.TransformPoint(new Vector3(0, 0 + (rightStick.y * -yLookAmount), 0)));
                    else
                        cam.transform.LookAt(player.TransformPoint(new Vector3(0, 0, 0)));
                }
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
        } else
        {
            if (!death)
            {
                cam.transform.position = Vector3.Lerp(transform.position, lockOnParent.transform.position, 60 * Time.deltaTime);
                cam.transform.rotation = Quaternion.Slerp(transform.rotation, lockOnParent.transform.rotation, 60 * Time.deltaTime);
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
