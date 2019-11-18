using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{

    bool jump;
    bool delay = false;
    public float force;
    public Health h;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (jump)
        {
            if (!delay)
            if(Input.GetButtonDown("PadA" + h.playerNum.ToString())) {
                rb.AddForce(Vector3.up * force);
                    StartCoroutine(JumpDelay());
            }
        } 
    }


    private void OnTriggerEnter(Collider other)
    {
        jump = true;       
    }

    private void OnTriggerExit(Collider other)
    {
        jump = false;
    }

    IEnumerator JumpDelay ()
    {
        delay = true;
        yield return new WaitForSeconds(1);
        delay = false;
    }
}
