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



    bool rightFiredLast;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        model.SetActive(true);
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
        PadRB = value.Get<float>();
        if (PadLB == 0)
        {
            if (!rightFiredLast)
            {

                FireRightSide();
            }
            else
            {
                FireLeftSide();

            }
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
}
