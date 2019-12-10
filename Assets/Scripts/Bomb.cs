using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombDelay; //how many seconds it takes for the bomb to explode
    public float maxRange; //the furthest distance away in which objects get effected by the explosion
    public float force; //the force exerted on objects in the explosion effect
    public float damage; //damage done by the explosion

    Animation anim;
    AudioSource audio;
    Rigidbody rb;
    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ExplodeAfterTime());
        anim = GetComponent<Animation>();
        audio = GetComponent<AudioSource>();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();

        rb.constraints = RigidbodyConstraints.FreezePositionX |
                            RigidbodyConstraints.FreezePositionY |
                            RigidbodyConstraints.FreezePositionZ;
    }

    void Explode()
    {
        
        StopAllCoroutines();
        anim.Play();
        audio.Play();
        particles.Play();
        Invoke("DamageAndForce",.1f);
        Invoke("DestroyThisGameObject",1f);
    }

    void DamageAndForce() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRange); //gets an array of all the colliders within maxRange units
        foreach (Collider c in hitColliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(force * rb.mass, transform.position, maxRange);

            Health h = c.GetComponent<Health>();
            if (h != null)
                h.health -= damage;
        }
    }

    IEnumerator ExplodeAfterTime ()
    {
       /* rb.constraints = RigidbodyConstraints.FreezePositionX |
                            RigidbodyConstraints.FreezePositionY |
                            RigidbodyConstraints.FreezePositionZ; */

        yield return new WaitForSeconds(bombDelay);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRange); //gets an array of all the colliders within maxRange units
        foreach (Collider c in hitColliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(force * rb.mass, transform.position, maxRange); 

            Health h = c.GetComponent<Health>();
            if (h != null)
                h.health -= damage;
        }
        anim.Play();
        audio.Play();
        Invoke("DestroyThisGameObject", 1f);
    }

    void DestroyThisGameObject()
    {
        Destroy(this.gameObject);
    }
}
