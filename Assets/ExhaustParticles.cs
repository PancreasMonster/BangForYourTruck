using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustParticles : MonoBehaviour
{
    

    public void ExhaustParticlesPlay()
    {
        for (int z = 0; z < 4; z++)
        {
            transform.GetChild(z).GetComponent<ParticleSystem>().Play();
        }

    } 
}
