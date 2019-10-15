using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public Transform mbase;
    Vector3 offset;
    protected Vector3 localStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        offset = mbase.position - transform.position;
        localStartPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mbase.transform.position - offset;
    }
}
