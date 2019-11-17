using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlayerToStartPos : MonoBehaviour
{
    public GameObject player1;
    Vector3 player1StartPos;

    public GameObject player2;
    Vector3 player2StartPos;


    // Start is called before the first frame update
    void Start()
    {
        player1StartPos = new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z);
        player2StartPos = new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1)
        {
            player1.transform.position = player1StartPos;
            player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player1.GetComponent<FlagHolder>().FallOff();
        }

        if (other.gameObject == player2)
        {
            player2.transform.position = player2StartPos;
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player2.GetComponent<FlagHolder>().FallOff();
        }
    }
}
