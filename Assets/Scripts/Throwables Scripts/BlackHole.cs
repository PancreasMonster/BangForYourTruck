using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    bool activated;
    Rigidbody rb;
    public float timeActive = 0f;
    public float rangeMultiplier;
    public float duration;
    public float strength;
    public float maxRange = 2400, minRange = 800;
    float range;
    float explosiveRadius = 100f;
    public float damage;
    public float explosiveForce;
    MoonGravity gravity;
    public ParticleSystem vacuumParticle;

    public GameObject source;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<MoonGravity>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (activated)
        {
            timeActive += Time.deltaTime;
            range = Mathf.Max(maxRange * ((timeActive / duration)), minRange);
            GetComponent<MoonGravity>().maxDistance = range;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<MoonGravity>().enabled = true;
        GetComponent<MoonGravity>().force = strength;
        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ;
        vacuumParticle.Play();
        activated = true;
        Invoke("ExplodingFinish", duration);
    }

    void ExplodingFinish()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        gravity.enabled = false;
        foreach (Collider c in hitColliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosiveForce * rb.mass, transform.position, explosiveRadius);

            Health h = c.GetComponent<Health>();
            if (h != null)
                h.TakeDamage("black holed", source, damage, Vector3.zero);
        }
        transform.GetChild(0).gameObject.SetActive(false);
        Invoke("DestroyThisGameObject", 1f);
    }

    void DestroyThisGameObject()
    {
        Destroy(this.gameObject);
    }
}
