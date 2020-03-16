using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : MonoBehaviour
{
    public GameObject model;
    public float force;
    Transform ShotgunWeaponFiringPoint;
    Transform ShotgunWeaponFiringPoint2;
    public GameObject weaponProjectile;
    PowerHolder ph;
    public PowerCosts pc;
    ParticleSystem particles1;
    ParticleSystem particles2;

    bool rightFiredLast;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        model.SetActive(true);
        ShotgunWeaponFiringPoint = transform.Find("Trails and firing points").transform.Find("ShotgunWeaponFiringPoint").transform;
        ShotgunWeaponFiringPoint2 = transform.Find("Trails and firing points").transform.Find("ShotgunWeaponFiringPoint2").transform;
        particles1 = ShotgunWeaponFiringPoint.gameObject.GetComponent<ParticleSystem>();
        particles2 = ShotgunWeaponFiringPoint2.gameObject.GetComponent<ParticleSystem>();
        ph = GetComponent<PowerHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PadRB" + GetComponent<Health>().playerNum.ToString()))
        {
            if (!rightFiredLast)
            {

                FireBullet();
            }
            else
            {
                FireBullet2();

            }
        }
    }

    void FireBullet()
    {
        if (ph.powerAmount >= pc.powerCosts[5])
        {

            GameObject Bullet = Instantiate(weaponProjectile, ShotgunWeaponFiringPoint.position, ShotgunWeaponFiringPoint.transform.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            Bullet.GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity;

            Bullet.GetComponent<ShotgunBullet>().teamNum = GetComponent<Health>().teamNum;

            Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Bullet.GetComponent<ShotgunBullet>().damageSource = this.gameObject;

            particles1.Play();

            ph.losePower(pc.powerCosts[5]);

            rightFiredLast = true;
            //Debug.Log("right fired");
        }
    }

    void FireBullet2()
    {
        if (ph.powerAmount >= pc.powerCosts[5])
        {

            GameObject Bullet = Instantiate(weaponProjectile, ShotgunWeaponFiringPoint2.position, ShotgunWeaponFiringPoint2.transform.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            Bullet.GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity;

            Bullet.GetComponent<ShotgunBullet>().teamNum = GetComponent<Health>().teamNum;

            Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Bullet.GetComponent<ShotgunBullet>().damageSource = this.gameObject;

            particles2.Play();

            ph.losePower(pc.powerCosts[5]);

            rightFiredLast = false;
          //  Debug.Log("left fired");

        }
    }
}
