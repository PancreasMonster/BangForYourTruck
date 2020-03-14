using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public int teamNum;
    public GameObject damageSource; //the source of this GameObject ie. the player that instantiated the 'bullet' 
    public float damageToDeal;
    public float maxDamage;
    public float bulletLifetime;
    //public float growthOverTime;
    public float damageFallOffOvertTime;

    public bool player;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DestroyAfterLifetime>().lifetime = bulletLifetime;
        damageToDeal = maxDamage;

        if (player)
            damageSource = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        bulletLifetime -= Time.deltaTime;
        if (bulletLifetime <= 0f)
        {
            Destroy(this.gameObject);
        }

        damageToDeal = damageToDeal - (Time.deltaTime * damageFallOffOvertTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Health>() != null && other.transform.GetComponent<Health>().teamNum != teamNum)
        {
            other.transform.GetComponent<Health>().TakeDamage(null, damageSource, damageToDeal, Vector3.zero);
        }
    }
}
