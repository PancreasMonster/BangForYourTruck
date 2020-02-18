using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerWeapon : MonoBehaviour
{
    public float fireRate;
    public float force;
    public Transform discFiringPoint;
    public Transform discFiringPoint2;
    public GameObject weaponProjectile;
    PowerHolder ph;
    public PowerCosts pc;

    // Start is called before the first frame update
    void Start()
    {
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
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

            GameObject Disc = Instantiate(weaponProjectile, discFiringPoint.position, weaponProjectile.transform.rotation);
            Disc.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().playerNum;
            Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);
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

            GameObject Disc = Instantiate(weaponProjectile, discFiringPoint2.position, weaponProjectile.transform.rotation);
            Disc.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().playerNum;
            Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);
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
