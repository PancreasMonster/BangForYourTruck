using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    Rigidbody rb;
    public float duration;
    public float strength;
    public float range;
    float explosiveRadius = 100f;
    public float damage;
    public float explosiveForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<MoonGravity>().enabled = true;
        GetComponent<MoonGravity>().force = strength;
        GetComponent<MoonGravity>().maxDistance = range;
        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ;

        Invoke("ExplodingFinish", duration);
    }

    void ExplodingFinish()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        GetComponent<MoonGravity>().enabled = false;
        foreach (Collider c in hitColliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosiveForce * rb.mass, transform.position, explosiveRadius);

            Health h = c.GetComponent<Health>();
            if (h != null)
                h.health -= damage;
        }
        transform.GetChild(0).gameObject.SetActive(false);
        Invoke("DestroyThisGameObject", 1f);
    }

    void DestroyThisGameObject()
    {
        Destroy(this.gameObject);
    }
}
