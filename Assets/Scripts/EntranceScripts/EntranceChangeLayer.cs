﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceChangeLayer : MonoBehaviour
{
    public int layerNum;
    public bool turnOffBoost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.transform.tag == "Player")
        {
            coll.gameObject.layer = layerNum;
            foreach(Transform t in coll.transform)
            {
                t.gameObject.layer = layerNum;
            }
            if(turnOffBoost)
            {
                coll.gameObject.GetComponent<PropelSelf>().canBoost = false;
            } else
            {
                coll.gameObject.GetComponent<PropelSelf>().canBoost = true;
            }
        }
    }
}