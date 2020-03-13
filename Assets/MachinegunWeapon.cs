using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunWeapon : MonoBehaviour
{
    public float lockOnRange;
    public GameObject model;
    public Animator anim;
    public float fireRate;
    public float force;
    public Transform AutoWeaponFiringPoint;
    public Transform AutoWeaponFiringPoint2;
    public GameObject weaponProjectile;
    PowerHolder ph;
    public PowerCosts pc;
    ParticleSystem particles1;
    ParticleSystem particles2;


    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        GetComponent<LockOn>().maxDistance = lockOnRange;
        model.SetActive(true);

        particles1 = transform.Find("Trails and firing points").transform.Find("AutoWeaponFiringPoint").GetComponent<ParticleSystem>();
        particles2 = transform.Find("Trails and firing points").transform.Find("AutoWeaponFiringPoint2").GetComponent<ParticleSystem>();
        ph = GetComponent<PowerHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PadRB" + GetComponent<Health>().playerNum.ToString()))
        {


            FireBullet();
            InvokeRepeating("FireBullet2", fireRate / 2, fireRate);
            InvokeRepeating("FireBullet", fireRate, fireRate);
            anim.SetBool("Spinning", true);
            // triggerDown = false;
            // t = 0;
            // power = 0;
            //  fill.fillAmount = 0;
            //  bg.gameObject.SetActive(false);
            // rh.resourceAmount -= rc.resourceCosts[currentI];

        }

        if (Input.GetButtonUp("PadRB" + GetComponent<Health>().playerNum.ToString()))
        {
            CancelInvoke();
            anim.SetBool("Spinning", false);
        }
    }


    void FireBullet()
    {
        if (ph.powerAmount >= pc.powerCosts[2])
        {

            GameObject Bullet = Instantiate(weaponProjectile, AutoWeaponFiringPoint.position, AutoWeaponFiringPoint.transform.rotation);

            Bullet.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().teamNum;

            Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Bullet.GetComponent<CollisionDamage>().damageSource = this.gameObject;
         
            particles1.Play();

            ph.losePower(pc.powerCosts[2]);

        }
        else
        {
            CancelInvoke();
            anim.SetBool("Spinning", false);
        }
    }

    void FireBullet2()
    {
        if (ph.powerAmount >= pc.powerCosts[2])
        {

            GameObject Bullet = Instantiate(weaponProjectile, AutoWeaponFiringPoint2.position, AutoWeaponFiringPoint2.transform.rotation);

            Bullet.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().teamNum;

            Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Bullet.GetComponent<CollisionDamage>().damageSource = this.gameObject;

            particles2.Play();

            ph.losePower(pc.powerCosts[2]);

        }
        else
        {
            CancelInvoke();
            anim.SetBool("Spinning", false);
        }
    }
}
