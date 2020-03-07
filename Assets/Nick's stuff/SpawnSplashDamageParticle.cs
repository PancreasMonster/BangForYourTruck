using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSplashDamageParticle : MonoBehaviour
{
    public GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        SpawnParticle();
    }

    public void SpawnParticle() {
        Instantiate(particle, transform.position, transform.rotation);
    }
}
