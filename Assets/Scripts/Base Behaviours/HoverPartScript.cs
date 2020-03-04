using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPartScript : MonoBehaviour
{
    public Transform wheelTarget;
    public ParticleSystem ps;
    float origDistance;

    // Start is called before the first frame update
    void Start()
    {
        origDistance = Vector3.Distance(transform.position, wheelTarget.position);
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(wheelTarget);
    }
}
