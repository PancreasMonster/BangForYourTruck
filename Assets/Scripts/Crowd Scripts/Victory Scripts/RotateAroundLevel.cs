using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundLevel : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform level;
    public Vector3 offset;
    private Vector3 origPos;  
    public float lookOffsetY = 3;
    

    void Start()
    {
        origPos = offset;    
    }

    

    void Update()
    {        
        transform.position = level.position + offset;
        offset = Quaternion.AngleAxis(turnSpeed * Time.deltaTime, Vector3.up) * offset;            
        transform.LookAt(new Vector3(level.position.x, level.position.y + lookOffsetY, level.position.z));          
    }
            

}
