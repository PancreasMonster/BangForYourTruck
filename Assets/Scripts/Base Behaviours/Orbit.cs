using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player, childPos;
    public int playerNum;

    private Vector3 offset;
    private Vector3 origPos;
    private bool lockedBehind = true;

    void Start()
    {
        offset = transform.position - player.transform.position;
        origPos = offset;
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
            transform.position = new Vector3(childPos.position.x, player.position.y + 10, childPos.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            transform.LookAt(player.position);
        }
       
        if (!lockedBehind)
        {
            transform.position = player.position + offset;
            offset = Quaternion.AngleAxis(Input.GetAxisRaw("RHorizontal" + playerNum.ToString()) * turnSpeed, Vector3.up) * offset;
            
        }
       
    }
}
