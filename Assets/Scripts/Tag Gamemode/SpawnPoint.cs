using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int playerNum;
    GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;

        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject t in players)
        {

            if (t.GetComponent<Health>().playerNum == playerNum)
            {
                t.transform.position = transform.position;
                t.transform.rotation = transform.rotation;
            }
        }
    }
}
