using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWeapon : MonoBehaviour
{
    bool charging;
    bool onCooldown;
    public float cooldownTime;
    float chargingTime;
    public float maxChargeTime;
    public float force;
    float startForce;
    public Transform cannonFiringPoint;
    public GameObject weaponProjectile;
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

        if (charging == true) {
            chargingTime += Time.deltaTime;
        }

        if (chargingTime >= maxChargeTime)
        {
            Firecannon();

        }

        if (Input.GetButtonUp("PadRB" + GetComponent<Health>().playerNum.ToString()) && charging == true)
        {
            Firecannon();

        }
    }


    void Firecannon() {
        force = force * (1 + chargingTime/2);
            if (ph.powerAmount >= pc.powerCosts[2])
            {

                GameObject Disc = Instantiate(weaponProjectile, cannonFiringPoint.position, weaponProjectile.transform.rotation);
                Disc.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().playerNum;
                Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);
                if (Disc.GetComponent<ResourceCollection>() != null)
                    Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
                if (Disc.GetComponent<Health>() != null)
                    Disc.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
                ph.losePower(pc.powerCosts[2]);
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
