using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWeapon : MonoBehaviour
{
    GameObject target;
    public float range;
    public float damage;
    bool charging;
    bool onCooldown;
    public float cooldownTime;
    float chargingTime;
    public float maxChargeTime;
    float force;
    float startForce;
    public Transform cannonFiringPoint;
    PowerHolder ph;
    public PowerCosts pc;

    // Start is called before the first frame update
    void Start()
    {
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        startForce = force;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PadRB" + GetComponent<Health>().playerNum.ToString()))
        {
            if (!onCooldown)
            {
                //begin charging
                charging = true;
                chargingTime = 0f;
            }


        }

        if (charging == true)
        {
            chargingTime += Time.deltaTime;
        }

        if (chargingTime >= maxChargeTime)
        {
            FireLightning();

        }

        if (Input.GetButtonUp("PadRB" + GetComponent<Health>().playerNum.ToString()) && charging == true)
        {
            FireLightning();

        }
    }


    void FireLightning()
    {
        //force is used as a multiplyer to the cost to fire this weapon from 0.1 to 1.5
        force = force * chargingTime / 2;

        if (ph.powerAmount >= pc.powerCosts[2] * force)
        {
            float rangeOfAttack = range * force;
            float damageToDeal = damage * force;

            //we need to reference a locked on enemy to hit first, they must be in range(<rangeOfAttack in distance) and if they are they take
            //lose hp = to damageToDeal. If they are out of range

            float distanceToTarget = Vector3.Distance(target.transform.position, this.gameObject.transform.position);

            

            if (distanceToTarget >= rangeOfAttack) {
                target.GetComponent<Health>().health = target.GetComponent<Health>().health - damageToDeal;
            }

            //we reduce the range and damage here before feeding them to the enemy to continue the chain effect
            rangeOfAttack = rangeOfAttack * .75f;
            damageToDeal = damageToDeal * .75f;

            //Now the attack will jump to another enemy in range. The enemy hit will need to check for any of their allies in range of them 
            //(<rangeOfAttack in distance) and hit the closest enemy to them for damageToDeal. 

            ph.losePower(pc.powerCosts[2] * force);
            force = startForce;
            chargingTime = 0f;
            charging = false;
            onCooldown = true;
            Invoke("Cooldown", cooldownTime);
        }
    }

    void Cooldown()
    {
        onCooldown = false;

    }
}
