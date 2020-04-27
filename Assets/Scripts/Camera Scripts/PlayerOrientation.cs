using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    public Transform p;
    FlipOver fo;

    // Start is called before the first frame update
    void Start()
    {
        fo = p.GetComponent<FlipOver>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = p.position;
        transform.eulerAngles = new Vector3(0, p.eulerAngles.y, 0);

        transform.up = Vector3.Lerp(transform.up, fo.hit.normal, 60 * Time.deltaTime);
        transform.Rotate(0, p.eulerAngles.y, 0);
    }
}
