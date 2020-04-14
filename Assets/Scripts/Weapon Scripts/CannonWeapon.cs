using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonWeapon : MonoBehaviour
{
    public float arcOffset;
    public float lockOnRange;
    public GameObject model;
    public GameObject engineModel;
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
    ParticleSystem particle;
    public bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LockOn>().maxDistance = lockOnRange;

        particle = cannonFiringPoint.GetComponent<ParticleSystem>();
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        startForce = force;

        if (canFire)
        {
            model.SetActive(true);
        }
        else
        {
            engineModel.SetActive(true);
        }
    }

    float PadLB;
    float PadRB;

    private void OnLeftBumper(InputValue value)
    {
        PadLB = 1;
    }

    private void OnLeftBumperRelease(InputValue value)
    {
        PadLB = 0;
    }

    private void OnRightBumper(InputValue value)
    {
        if (canFire && PadLB == 0)
        {
            PadRB = 1;
            if (!onCooldown && ph.powerAmount >= pc.powerCosts[6])
            {
                //begin charging
                charging = true;
                chargingTime = 0f;
            }
            //Firecannon();

        }

    }

    private void OnRightBumperRelease(InputValue value)
    {

        if (charging == true)
        {
            Firecannon();
            model.GetComponent<Animation>().Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PadRB > 0 && PadLB == 0)
        {
           

        }

        if (charging == true) {
          chargingTime += Time.deltaTime;
        }

        if (chargingTime >= maxChargeTime)
        {
            chargingTime = maxChargeTime;
            Firecannon();

        }

        if (PadRB > 0 && charging == true)
        {
            Firecannon();
            model.GetComponent<Animation>().Play();
        }
    }


    void Firecannon() {
        force = force * (1 + chargingTime/2);
            if (ph.powerAmount >= pc.powerCosts[6])
            {
                arcOffset = chargingTime * 10f;
                GameObject Disc = Instantiate(weaponProjectile, cannonFiringPoint.position, weaponProjectile.transform.rotation);
                Disc.GetComponent<CannonBallScript>().teamNum = GetComponent<Health>().teamNum;
                particle.Play();

       
                Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);

                Disc.GetComponent<CannonBallScript>().damageSource = this.gameObject;

                ph.losePower(pc.powerCosts[6]);
                force = startForce;
            chargingTime = 0f;
            charging = false;
            //onCooldown = true;
            //Invoke("Cooldown", cooldownTime);
            }
        }

    void Cooldown()
    {
        onCooldown = false;

    }
}
