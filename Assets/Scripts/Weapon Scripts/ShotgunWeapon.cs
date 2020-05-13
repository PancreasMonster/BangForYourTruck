using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotgunWeapon : MonoBehaviour
{
    public GameObject model;
    public float force;
    Transform ShotgunWeaponFiringPoint;
    Transform ShotgunWeaponFiringPoint2;
    public GameObject weaponProjectile;
    PowerHolder ph;
    public PowerCosts pc;
    ParticleSystem buckshotParticlesRight;
    ParticleSystem emptyShellsRight;
    Animation rightShotgunAnim;
    ParticleSystem buckshotParticlesLeft;
    ParticleSystem emptyShellsLeft;
    Animation leftShotgunAnim;
    public bool canFire;
    public float fireRate;
    bool cooldown;

    bool rightFiredLast;

    PlayerPause pp;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        GameObject leftShotGun = model.transform.Find("Shotgun Left").gameObject;
        GameObject rightShotGun = model.transform.Find("Shotgun Right").gameObject;
        leftShotgunAnim = leftShotGun.GetComponent<Animation>();
        rightShotgunAnim = rightShotGun.GetComponent<Animation>();
        emptyShellsRight = rightShotGun.GetComponent<ParticleSystem>();
        emptyShellsLeft = leftShotGun.GetComponent<ParticleSystem>();
        ShotgunWeaponFiringPoint = transform.Find("Trails and firing points").transform.Find("ShotgunWeaponFiringPoint").transform;
        ShotgunWeaponFiringPoint2 = transform.Find("Trails and firing points").transform.Find("ShotgunWeaponFiringPoint2").transform;
        buckshotParticlesRight = ShotgunWeaponFiringPoint.gameObject.GetComponent<ParticleSystem>();
        buckshotParticlesLeft = ShotgunWeaponFiringPoint2.gameObject.GetComponent<ParticleSystem>();
        ph = GetComponent<PowerHolder>();

        if (canFire)
        {
            model.SetActive(true);
        }

        pp = GetComponent<PlayerPause>();
    }

    float PadLB;
    float PadRB;

    private void OnLeftBumper(InputValue value)
    {
        if (!pp.noPlayerInput)
        
            PadLB = 1;
    }

    private void OnLeftBumperRelease(InputValue value)
    {
        if (!pp.noPlayerInput)
        
            PadLB = 0;
    }

    private void OnRightBumper(InputValue value)
    {
        if (!pp.noPlayerInput)
        {
            PadRB = value.Get<float>();
            if (canFire && !cooldown)
            {
                if (PadLB == 0)
                {
                    if (!rightFiredLast)
                    {

                        FireRightSide();
                        cooldown = true;
                        StartCoroutine(Cooldown(fireRate));
                    }
                    else
                    {
                        FireLeftSide();
                        cooldown = true;
                        StartCoroutine(Cooldown(fireRate));
                    }
                }
            }
        }
        else
        {
            PadRB = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PadRB > 0 && PadLB == 0)
        {
            
        }
    }

    void FireRightSide()
    {
        if (ph.powerAmount >= pc.powerCosts[5])
        {

            GameObject Bullet = Instantiate(weaponProjectile, ShotgunWeaponFiringPoint.position, ShotgunWeaponFiringPoint.transform.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            Bullet.GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity;

            Bullet.GetComponent<ShotgunBullet>().teamNum = GetComponent<Health>().teamNum;

            Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Bullet.GetComponent<ShotgunBullet>().damageSource = this.gameObject;

            buckshotParticlesRight.Play();
            emptyShellsRight.Play();
            rightShotgunAnim.Play();
            ph.losePower(pc.powerCosts[5]);

            rightFiredLast = true;
            //Debug.Log("right fired");
        }
    }

    void FireLeftSide()
    {
        if (ph.powerAmount >= pc.powerCosts[5])
        {

            GameObject Bullet = Instantiate(weaponProjectile, ShotgunWeaponFiringPoint2.position, ShotgunWeaponFiringPoint2.transform.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            Bullet.GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity;

            Bullet.GetComponent<ShotgunBullet>().teamNum = GetComponent<Health>().teamNum;

            Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Bullet.GetComponent<ShotgunBullet>().damageSource = this.gameObject;

            buckshotParticlesLeft.Play();
            emptyShellsLeft.Play();
            leftShotgunAnim.Play();
            ph.losePower(pc.powerCosts[5]);

            rightFiredLast = false;
          //  Debug.Log("left fired");

        }
    }

    IEnumerator Cooldown(float time) 
    {
        yield return new WaitForSeconds(time);
        cooldown = false;
    }
}
