﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralTagPickUp : MonoBehaviour
{
    bool collected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (!collected)
        {
            if (coll.transform.tag == "Player")
            {
                TagHolder t = coll.transform.GetComponent<TagHolder>();
                if (t.currentTags < 3)
                {
                    collected = true;
                    t.currentTags++;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}