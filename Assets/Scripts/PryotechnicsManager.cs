using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PryotechnicsManager : MonoBehaviour
{
    public ParticleSystem[] redTeamParticles;
    public ParticleSystem[] blueTeamParticles;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            RedTeamParticlesFire();
        }

        if (Input.GetKeyDown("y"))
        {
            BlueTeamParticlesFire();
        }
    }

    public void RedTeamParticlesFire()
    {
        for(int i = 0; i < redTeamParticles.Length; i++)
        {
            redTeamParticles[i].Play();
        }
    }

    public void BlueTeamParticlesFire()
    {
        for (int i = 0; i < blueTeamParticles.Length; i++)
        {
            blueTeamParticles[i].Play();
        }
    }
}
