using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject parentObject, target;
    public float yOffset = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(parentObject.transform.position.x, parentObject.transform.position.y + yOffset, parentObject.transform.position.z), 60 * Time.deltaTime);
        if(target != null)
        transform.LookAt(target.transform);
    }
}
