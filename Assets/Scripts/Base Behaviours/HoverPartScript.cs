using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPartScript : MonoBehaviour
{
    public Transform wheelTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(wheelTarget);
    }
}
