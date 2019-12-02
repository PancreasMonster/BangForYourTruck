using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player, hoverBox;
    public int playerNum;
    public float timeAllowance;
    public LayerMask layer;
    public List<Transform> wheelVisuals = new List<Transform>();
    private RaycastHit hit;
    private float timer;
    public Vector3 offset = new Vector3(0, 10, -26);
    private Vector3 origPos;
    private bool lockedBehind = true;
    private bool disorient;
    public FlipOver fo;
    public float setDrag = 3;
    public float animSpeed;
    private WheelCollider[] wheels;
    public bool death;

    void Start()
    {
        //offset = new Vector3(0, 10, -26);
        origPos = offset;
        wheels = player.GetComponentsInChildren<WheelCollider>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("PadY" + playerNum.ToString()) && !death)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.drag = setDrag;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0);
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
            GetComponent<Orbit>().enabled = false;
        }
    }

    void LateUpdate()
    {
        if (!death)
        {
            Vector3 dir = player.position - transform.position;
            dir.Normalize();
            if (Input.GetButtonDown("RightStick" + player.GetComponent<Health>().playerNum.ToString()))
            {
                lockedBehind = !lockedBehind;
                offset = transform.position - player.transform.position;
            }
            if (lockedBehind)
            {


                if (fo.timer > timeAllowance)
                {
                    if (!disorient)
                    {
                        offset = transform.position - player.transform.position;
                        disorient = true;
                    }

                    transform.position = player.position + offset;
                    offset = Quaternion.AngleAxis(Input.GetAxisRaw("RHorizontal" + playerNum.ToString()) * turnSpeed * Time.deltaTime, Vector3.up) * offset;
                    transform.LookAt(player.position);
                }
                else
                {
                    transform.position = new Vector3(player.TransformPoint(origPos).x, player.position.y + 10, player.TransformPoint(origPos).z);
                    disorient = false;
                }

                transform.LookAt(player.position);
            }

            if (!lockedBehind)
            {
                transform.position = player.position + offset;
                offset = Quaternion.AngleAxis(Input.GetAxisRaw("RHorizontal" + playerNum.ToString()) * turnSpeed * Time.deltaTime, Vector3.up) * offset;
                transform.LookAt(player.position);
            }
        }
        else
        {
            transform.position = new Vector3(-1000, -1000, -1000);
               
        }
    }
}
