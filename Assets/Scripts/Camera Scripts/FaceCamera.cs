using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public bool BillboardX = true;
    public bool BillboardY = true;
    public bool BillboardZ = true;
    public float OffsetToCamera;
    public Transform mbase;
    Vector3 offset;
    protected Vector3 localStartPosition;

    // Use this for initialization
    void Start()
    {
        offset = mbase.position - transform.position;
        localStartPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mbase.transform.position - offset;

        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                                                               Camera.main.transform.rotation * Vector3.up);
        if (!BillboardX || !BillboardY || !BillboardZ)
            transform.rotation = Quaternion.Euler(BillboardX ? transform.rotation.eulerAngles.x : 0f, BillboardY ? transform.rotation.eulerAngles.y : 0f, BillboardZ ? transform.rotation.eulerAngles.z : 0f);
       // transform.localPosition = localStartPosition;
        //transform.position = transform.position + transform.rotation * Vector3.forward * OffsetToCamera;
    }
}
