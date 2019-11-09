using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundNormalisation : MonoBehaviour
{
    public float adjustSpeed = 1;
    Quaternion fromRotation;
    Quaternion toRotation;
    Vector3 targetNormal;
    RaycastHit hit;
    float weight = 9;
    public LayerMask layer;
  
  void Start()
    {
        targetNormal = transform.up;
    }

    void  Update()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 20, layer))
        {

            targetNormal = hit.normal;
            fromRotation = transform.rotation;
            toRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            weight = 0;


            if (weight <= 1)
            {
                weight += Time.deltaTime * adjustSpeed;
                transform.rotation = Quaternion.Slerp(fromRotation, toRotation, weight);

            }
        }
    }
}
