using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretParticleDamage : MonoBehaviour
{
    public float damageToDeal;

    

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<Health>() != null && other.gameObject.GetComponent<Health>().teamNum != GetComponentInParent<Health>().teamNum)
        {

            other.gameObject.GetComponent<Health>().health -= damageToDeal;
        }
    }
}
