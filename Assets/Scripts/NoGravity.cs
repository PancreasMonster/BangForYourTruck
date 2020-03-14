using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravity : MonoBehaviour
{
    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject t in players)
        {
            t.GetComponent<FlipOver>().fakeGravity = false;
        }

    }
}
