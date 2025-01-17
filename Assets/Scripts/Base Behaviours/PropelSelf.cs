﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.InputSystem;

public class PropelSelf : MonoBehaviour
{
    public float force;
    public Image bg, fill, bgcd, fillcd;  //cd = cooldown
    public float cooldownDelay;    Rigidbody rb;
    bool triggerDown = false, coolingDown = false;
    public PostProcessVolume PPV;
    public ParticleSystem psMid, psLeft, psRight;
    float t, power;
    PowerHolder ph;
    PowerCosts pc;
    RaycastHit hit;
    public LayerMask layer;
    public float limitingForce = .75f;
    public Orbit orb;
    MobilityCharges mobCharges;
    public float targetBoostMaxDistance = 100;
    public bool canBoost = true;
    public Animator anim;
    public float boostDurationFactor;
    PlayerPause pp;

    // Start is called before the first frame update
    void Start()
    {
        mobCharges = GetComponent<MobilityCharges>();
        rb = GetComponent<Rigidbody>();
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        pp = GetComponent<PlayerPause>();
    }

    float BButton = 1;

    private void OnFaceButtonEast(InputValue value)
    {
        if (canBoost && !pp.noJumpOrBoost)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), -transform.up * 5, out hit, 5, layer))
            {

                if (!coolingDown)
                {
                    if (mobCharges.currentCharges > 0)
                    {

                            rb.AddForce(transform.forward * force);
                        

                        anim.SetTrigger("Boost 0");
                        Debug.Log("Boosted");

                        StartCoroutine(BoostEffect());
                        triggerDown = false;
                        coolingDown = true;
                        mobCharges.UseCharge();
                        BroadcastMessage("ExhaustParticlesPlay");
                        StartCoroutine(Cooldown());
                    }
                    else
                    {
                        triggerDown = false;
                        coolingDown = true;
                        StartCoroutine(Cooldown());
                    }
                }
                else
                {
                   /* if (mobCharges.currentCharges > 0)
                    {
                        Vector3 dir = (8 * transform.forward) + (7 * transform.up);
                        dir.Normalize();
                        rb.AddForce(dir * force * limitingForce  * power );
                        //rb.angularVelocity = Vector3.zero;
                        // Debug.Log(dir);
                        anim.SetBool("Boost", true);
                        Debug.Log("Boosted");

                        StartCoroutine(BoostEffect());
                        triggerDown = false;
                        coolingDown = true;
                        mobCharges.UseCharge();
                        BroadcastMessage("ExhaustParticlesPlay");
                        StartCoroutine(Cooldown());
                    }
                    else
                    {
                        triggerDown = false;
                        coolingDown = true;
                        StartCoroutine(Cooldown());
                    }*/
                }

            }
            else
            {

                if (mobCharges.currentCharges > 0)
                {
                    //rb.velocity = Vector3.zero;

                        rb.AddForce(transform.forward * force);
                    
                    // if(orb.enabled == true)

                    StartCoroutine(BoostEffect());
                    triggerDown = false;
                    coolingDown = true;
                    mobCharges.UseCharge();
                    BroadcastMessage("ExhaustParticlesPlay");
                    StartCoroutine(Cooldown());
                }
                else
                {
                    triggerDown = false;
                    coolingDown = true;
                    StartCoroutine(Cooldown());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetButtonDown("PadA" + GetComponent<Health>().playerNum.ToString()) && !triggerDown && !coolingDown)
         {
             triggerDown = true;
             bg.gameObject.SetActive(true);
         }

         if (triggerDown)
         {
             t += Time.deltaTime;
             power = Mathf.Pow((Mathf.Sin(t)), 2);
             fill.fillAmount = power;
         } */

        if (coolingDown)
        {
            rb.AddForce(transform.forward * (force/ boostDurationFactor) * Time.deltaTime);
            Debug.Log("Boosting");
        }
    }

    IEnumerator Cooldown ()
    {
        //bgcd.gameObject.SetActive(true);
        // fillcd.fillAmount = 1;
        // while (fillcd.fillAmount > 0)
        //   {
        //      fillcd.fillAmount -= Time.deltaTime / cooldownDelay;
        //        yield return null;
        //    }
        // bgcd.gameObject.SetActive(false);
        yield return new WaitForSeconds(cooldownDelay);
        coolingDown = false;
        //anim.SetBool("Boost",false);
    }
   

        IEnumerator BoostEffect ()
    {
        psMid.Play();
        psLeft.Play();
        psRight.Play();
        GetComponent<AudioSource>().pitch = .8f - (.4f * (1.0f - ((GetComponent<PowerHolder>().powerAmount - 40) / GetComponent<PowerHolder>().maxPower)));
        GetComponent<AudioSource>().Play();
       // ChromaticAberration ChromAberr = null;
       // PPV.profile.TryGetSettings(out ChromAberr);
       // DepthOfField dop = null;
      //  PPV.profile.TryGetSettings(out dop);
       /* while (ChromAberr.intensity.value <= 1)
        {
            ChromAberr.intensity.value += (10f * Time.deltaTime);
            dop.focalLength.value += (40f * Time.deltaTime);
            yield return null;
        }*/
        yield return new WaitForSeconds(.6f);
        psMid.Stop();
        psLeft.Stop();
        psRight.Stop();


      /*  while (ChromAberr.intensity.value >= 0)
        {
            ChromAberr.intensity.value -= (5 * Time.deltaTime);
            dop.focalLength.value -= (40f * Time.deltaTime);
            yield return null;
        } */
        //dop.focalLength.value = 260f;
    }
}
 