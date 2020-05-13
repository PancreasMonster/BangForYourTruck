using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public Transform followCam;
    public float positionLerpSpeed;
    public float rotationLerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followCam.position, positionLerpSpeed * Time.deltaTime); 
        transform.rotation = Quaternion.Slerp(transform.rotation, followCam.rotation, rotationLerpSpeed * Time.deltaTime);
        //transform.rotation = followCam.rotation;
    }
}

