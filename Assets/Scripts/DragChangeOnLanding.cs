using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragChangeOnLanding : MonoBehaviour
{
    public float newDrag;
    public float newMass;



    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 14)
        {
            GetComponent<Rigidbody>().drag = newDrag;
            GetComponent<Rigidbody>().mass = newMass;


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        {
            GetComponent<Rigidbody>().drag = 0;


        }
    }
}
