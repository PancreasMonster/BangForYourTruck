using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragChangeOnLanding : MonoBehaviour
{
    public float newDrag;

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 14)
        {
            GetComponent<Rigidbody>().drag = newDrag;

        }
    }
}
