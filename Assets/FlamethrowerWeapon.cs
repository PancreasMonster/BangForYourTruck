using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerWeapon : MonoBehaviour
{
    public float lockOnRange;
    public GameObject model;
    public float fireRate;
    public float force;
    public Transform AutoWeaponFiringPoint;
    public Transform AutoWeaponFiringPoint2;
    public GameObject weaponProjectile;
    PowerHolder ph;
    public PowerCosts pc;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LockOn>().maxDistance = lockOnRange;
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();

        model.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PadRB" + GetComponent<Health>().playerNum.ToString()))
        {
           

            FireBullet();
            InvokeRepeating("FireBullet2", fireRate / 2, fireRate);
            InvokeRepeating("FireBullet", fireRate, fireRate);

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
        }
    }


    void FireBullet()
    {
        if (ph.powerAmount >= pc.powerCosts[2])
        {

            GameObject Disc = Instantiate(weaponProjectile, AutoWeaponFiringPoint.position, weaponProjectile.transform.rotation);
            Disc.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().teamNum;
            if (GetComponent<LockOn>().target != null)
            {
                Vector3 dir = GetComponent<LockOn>().target.transform.position - AutoWeaponFiringPoint.position;
                dir.Normalize();
                Disc.GetComponent<Rigidbody>().AddForce(dir * force);
            }
            else
            {
                Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            }
            if (Disc.GetComponent<ResourceCollection>() != null)
                Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
            if (Disc.GetComponent<Health>() != null)
                Disc.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
            ph.losePower(pc.powerCosts[2]);

        }
        else
        {
            CancelInvoke();
        }
    }

    void FireBullet2()
    {
        if (ph.powerAmount >= pc.powerCosts[5])
        {

            GameObject Disc = Instantiate(weaponProjectile, AutoWeaponFiringPoint2.position, weaponProjectile.transform.rotation);
            Disc.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().teamNum;
            if (GetComponent<LockOn>().target != null)
            {
                Vector3 dir = GetComponent<LockOn>().target.transform.position - AutoWeaponFiringPoint.position;
                dir.Normalize();
                Disc.GetComponent<Rigidbody>().AddForce(dir * force);
            }
            else
            {
                Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            }
            if (Disc.GetComponent<ResourceCollection>() != null)
                Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
            if (Disc.GetComponent<Health>() != null)
                Disc.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
            ph.losePower(pc.powerCosts[5]);

        }
        else
        {
            CancelInvoke();
        }
    }
}
