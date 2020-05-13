using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public int teamNum;
    public GameObject damageSource; //the source of this GameObject ie. the player that instantiated the 'bullet' 
    public float damageToDeal;
    public float maxDamage;
    float startingBulletLifeTime;
    public float bulletLifetime;
    public float damageFallOffOverTime;

    public bool player;
    bool active;
    public Sprite damageImage;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        startingBulletLifeTime = bulletLifetime;
        Destroy(this.gameObject, 1f);   

        if (player)
            damageSource = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        bulletLifetime -= Time.deltaTime;

        if (bulletLifetime <= 0f && active)
        {
            active = false;
            GetComponent<BoxCollider>().enabled = false;
        }

        damageToDeal = maxDamage - ((startingBulletLifeTime - bulletLifetime) * damageFallOffOverTime);
        
    }

    void OnCollisionEnter (Collision other)
    {
        GetComponent<BoxCollider>().enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Health>() != null && other.transform.GetComponent<Health>().teamNum != teamNum)
        {
            other.transform.GetComponent<Health>().TakeDamage(damageImage, damageSource, damageToDeal, Vector3.zero);
        }
    }
}
