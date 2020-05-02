using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int teamNum;
    public bool EMP;
    public bool artillery;
    public float EMPDuration;
    public float bombDelay; //how many seconds it takes for the bomb to explode
    public float maxRange; //the furthest distance away in which objects get effected by the explosion
    public float force; //the force exerted on objects in the explosion effect
    public float damage; //damage done by the explosion

    Animation anim;
    AudioSource audio;
    Rigidbody rb;
    public GameObject particles;
    public Sprite damageImage;

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
        if (!EMP)
        {
            DamageExplosion();
        }
        else
        {
            EMPExplosion();
        }
        

        rb.constraints = RigidbodyConstraints.FreezePositionX |
                            RigidbodyConstraints.FreezePositionY |
                            RigidbodyConstraints.FreezePositionZ;
    }

    void EMPExplosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRange); //gets an array of all the colliders within maxRange units
        foreach (Collider c in hitColliders)
        {
            if (c.GetComponent<Health>() == true)
            {
                if (c.GetComponent<Health>().teamNum != teamNum)
                {
                    c.GetComponent<RearWheelDrive>().maxSpeed = 0f;
                    c.GetComponent<RearWheelDrive>().EMPDuration(EMPDuration);
                }               
            }
            if (artillery)
            TurnOffParticles();
        }
        if (!artillery)
        {
            anim.Play();
        }

        StopAllCoroutines();
        //audio.Play();
        Instantiate(particles, transform.position, transform.rotation);
        //particles.Play();
        this.gameObject.layer = 10;
        Invoke("DestroyThisGameObject", 1f);
    }

    void DamageExplosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRange); //gets an array of all the colliders within maxRange units
        foreach (Collider c in hitColliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(force * rb.mass, transform.position, maxRange);

            Health h = c.GetComponent<Health>();
            if (h != null)
                h.TakeDamage (damageImage, this.gameObject, damage, Vector3.zero);
        }

        if (artillery)
        {
            TurnOffParticles();
        }
        if (!artillery)
        {
            anim.Play();
        }


        StopAllCoroutines();
        //audio.Play();
        Instantiate(particles, transform.position, transform.rotation);
        //particles.Play();
        this.gameObject.layer = 10;
        Invoke("DestroyThisGameObject",1f);
    }

    /*void DamageAndForce() {
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
        if (artillery)
        TurnOffParticles();
    }*/

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

    void TurnOffParticles()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log("Turned off");
    }

    void DestroyThisGameObject()
    {
        Destroy(this.gameObject);
    }
}
