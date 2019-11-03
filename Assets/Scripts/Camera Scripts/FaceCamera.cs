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
    Camera cam;

    // Use this for initialization
    void Start()
    {
        offset = mbase.position - transform.position;
        localStartPosition = transform.localPosition;
    }

    public void Cam1 ()
    {
        cam = GameObject.Find("CameraBase1").GetComponent<Camera>();
        this.gameObject.layer = 12;
        foreach (Transform trans in GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 12;
        }
    }

    public void Cam2()
    {
        cam = GameObject.Find("CameraBase2").GetComponent<Camera>();
        this.gameObject.layer = 13;
        foreach (Transform trans in GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 13;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mbase.transform.position - offset;

        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
                                                               cam.transform.rotation * Vector3.up);
        if (!BillboardX || !BillboardY || !BillboardZ)
            transform.rotation = Quaternion.Euler(BillboardX ? transform.rotation.eulerAngles.x : 0f, BillboardY ? transform.rotation.eulerAngles.y : 0f, BillboardZ ? transform.rotation.eulerAngles.z : 0f);
       // transform.localPosition = localStartPosition;
        //transform.position = transform.position + transform.rotation * Vector3.forward * OffsetToCamera;
    }
}
