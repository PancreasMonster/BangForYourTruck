using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    public int teamNum;
    GameObject[] blackholeTargets;
    public float primeTime = 1.5f;
    bool primed = false, triggered = false;
    public float lifeTime = 15f;
    ParticleSystem particles;
    AudioSource audio;
    public GameObject source;
    

    public float maxRange; //the furthest distance away in which objects get effected by the explosion
    public float force; //the force exerted on objects in the explosion effect
    public float damage; //damage done by the explosion

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
        StartCoroutine(setPrime());
        Destroy(this.gameObject, lifeTime);
        blackholeTargets = GameObject.FindGameObjectsWithTag("BlackHole");
        foreach(GameObject b in blackholeTargets)
        {
            b.GetComponent<MoonGravity>().targets.Add(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (primed)
        {

            if (other.gameObject.GetComponent<Health>() != null && other.gameObject.GetComponent<Health>().teamNum != teamNum && !triggered)
            {
                Debug.Log("HIT Player");
                triggered = true;
                Invoke("DamageExplosion", .15f);
            }
        }
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
                h.TakeDamage ("Mined", source, damage, Vector3.zero);
        }
        particles.Play();
        audio.Play();
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(this.gameObject, 2f);
    }

        IEnumerator setPrime ()
    {
        yield return new WaitForSeconds(primeTime);
        primed = true;
    }
}

