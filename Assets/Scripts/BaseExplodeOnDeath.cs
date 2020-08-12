using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BaseExplodeOnDeath : MonoBehaviour
{
    
    public GameObject[] particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Explode()
    {
        print("turret exploded from baseexplodeondeath");
        GetComponent<AudioSource>().Play();
        for (int i = 0; i < particles.Length; i++) 
        {
            particles[i].gameObject.SetActive(true);

        }
    }
  
}
