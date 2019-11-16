using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player, childPos;
    public int playerNum;
    public float timeAllowance;
    public LayerMask layer;

    private RaycastHit hit;
    private float timer;
    private Vector3 offset;
    private Vector3 origPos;
    private bool lockedBehind = true;
    private bool disorient;

    void Start()
    {
        offset = new Vector3(0, 10, -26);
        origPos = new Vector3(0, 10, -26);
    }

    void LateUpdate()
    {
        
        float yRotation = childPos.eulerAngles.y;
        Vector3 dir = player.position - transform.position;
        dir.Normalize();
        if (Input.GetButtonDown("RightStick" + player.GetComponent<Health>().playerNum.ToString()))
        {
            lockedBehind = !lockedBehind;
            offset = transform.position - player.transform.position;
        }
        if (lockedBehind)
        {
            if (Physics.Raycast(player.transform.position, -transform.up, out hit, 5, layer))
            {
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }

            if (timer > timeAllowance)
            {
                  if (!disorient)
                  {
                    offset = transform.position - player.transform.position;
                      disorient = true;
                 }

                transform.position = player.position + offset;
                offset = Quaternion.AngleAxis(Input.GetAxisRaw("RHorizontal" + playerNum.ToString()) * turnSpeed, Vector3.up) * offset;
                transform.LookAt(player.position);
            }
            else
            {
                transform.position = new Vector3 (player.TransformPoint(origPos).x, player.position.y + 10, player.TransformPoint(origPos).z);
                disorient = false;
            }
            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            transform.LookAt(player.position);
        }
       
        if (!lockedBehind)
        {
            transform.position = player.position + offset;
            offset = Quaternion.AngleAxis(Input.GetAxisRaw("RHorizontal" + playerNum.ToString()) * turnSpeed, Vector3.up) * offset;
            transform.LookAt(player.position);
        }
       
    }
}
