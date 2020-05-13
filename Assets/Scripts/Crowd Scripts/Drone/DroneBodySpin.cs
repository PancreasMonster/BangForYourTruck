using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBodySpin : MonoBehaviour
{
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, rotateSpeed * Time.deltaTime); 
    }
}
