using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunWeapon : MonoBehaviour
{
    public Animator anim;
    public float fireRate;
    public float force;
    public Transform discFiringPoint;
    public Transform discFiringPoint2;
    public GameObject weaponProjectile;
    PowerHolder ph;
    public PowerCosts pc;
    ParticleSystem particles1;
    ParticleSystem particles2;


    // Start is called before the first frame update
    void Start()
    {
        particles1 = transform.Find("DiscFiringPoint").GetComponent<ParticleSystem>();
        particles2 = transform.Find("DiscFiringPoint2").GetComponent<ParticleSystem>();
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

            GameObject Disc = Instantiate(weaponProjectile, discFiringPoint.position, weaponProjectile.transform.rotation);
            Disc.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().playerNum;
            Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            if (Disc.GetComponent<ResourceCollection>() != null)
                Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
            if (Disc.GetComponent<Health>() != null)
                Disc.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
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

            GameObject Disc = Instantiate(weaponProjectile, discFiringPoint2.position, weaponProjectile.transform.rotation);
            Disc.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().playerNum;
            Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            if (Disc.GetComponent<ResourceCollection>() != null)
                Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
            if (Disc.GetComponent<Health>() != null)
                Disc.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
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
