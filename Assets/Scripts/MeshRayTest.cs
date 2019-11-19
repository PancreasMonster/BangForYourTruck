using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRayTest : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 10))
        {
            Debug.Log("Hit");
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
    }
}
