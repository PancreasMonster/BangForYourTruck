﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExplodeOnDeath : MonoBehaviour
{
    public ParticleSystem[] particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode()
    {

        StartCoroutine("Explosions");
        /*for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
            //Debug.Log("Exploded");
            if(GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();
        }*/
    }

    
}
