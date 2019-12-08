using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PropelSelf : MonoBehaviour
{
    public float force;
    public Image bg, fill, bgcd, fillcd;  //cd = cooldown
    public float cooldownDelay;    Rigidbody rb;
    bool triggerDown = false, coolingDown = false;
    public PostProcessVolume PPV;
    public ParticleSystem ps, psChild;
    float t, power;
    PowerHolder ph;
    PowerCosts pc;
    RaycastHit hit;
    public LayerMask layer;
    public float limitingForce = .75f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
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

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Vector3.down * 5, out hit, 5, layer))
        {
            if (Input.GetButtonDown("PadX" + GetComponent<Health>().playerNum.ToString()))
            {
                if (!coolingDown)
                {
                    if (ph.powerAmount >= pc.powerCosts[0])
                    {
                       
                        rb.AddForce(transform.forward * force /* * power */);
                        StartCoroutine(BoostEffect());
                        triggerDown = false;
                        t = 0;
                        power = 0;
                        fill.fillAmount = 0;
                        bg.gameObject.SetActive(false);
                        coolingDown = true;
                        ph.losePower(pc.powerCosts[0]);
                        StartCoroutine(Cooldown());
                    }
                    else
                    {
                        triggerDown = false;
                        t = 0;
                        power = 0;
                        fill.fillAmount = 0;
                        bg.gameObject.SetActive(false);
                        coolingDown = true;
                        StartCoroutine(Cooldown());
                    }
                }
                else {
                    if (ph.powerAmount >= pc.powerCosts[0])
                    {
                        Vector3 dir = (8 * transform.forward) + (7 * transform.up);
                        dir.Normalize();
                        rb.AddForce(dir * force * limitingForce /* * power */);
                        rb.angularVelocity = Vector3.zero;
                        Debug.Log(dir);
                        StartCoroutine(BoostEffect());
                        triggerDown = false;
                        t = 0;
                        power = 0;
                        fill.fillAmount = 0;
                        bg.gameObject.SetActive(false);
                        coolingDown = true;
                        ph.losePower(pc.powerCosts[0]);
                        StartCoroutine(Cooldown());
                    }
                    else
                    {
                        triggerDown = false;
                        t = 0;
                        power = 0;
                        fill.fillAmount = 0;
                        bg.gameObject.SetActive(false);
                        coolingDown = true;
                        StartCoroutine(Cooldown());
                    }
                }
            } 
        } else
        {
            if (Input.GetButtonDown("PadX" + GetComponent<Health>().playerNum.ToString()) && !coolingDown)
            {
                if (ph.powerAmount >= pc.powerCosts[0])
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(transform.forward * force /* * power */);
                    StartCoroutine(BoostEffect());
                    triggerDown = false;
                    t = 0;
                    power = 0;
                    fill.fillAmount = 0;
                    bg.gameObject.SetActive(false);
                    coolingDown = true;
                    ph.losePower(pc.powerCosts[0]);
                    StartCoroutine(Cooldown());
                }
                else
                {
                    triggerDown = false;
                    t = 0;
                    power = 0;
                    fill.fillAmount = 0;
                    bg.gameObject.SetActive(false);
                    coolingDown = true;
                    StartCoroutine(Cooldown());
                }
            }
        }
    }

    IEnumerator Cooldown ()
    {
        bgcd.gameObject.SetActive(true);
        fillcd.fillAmount = 1;
        while (fillcd.fillAmount > 0)
        {
            fillcd.fillAmount -= Time.deltaTime / cooldownDelay;
            yield return null;
        }
        bgcd.gameObject.SetActive(false);
        coolingDown = false;
    }

    IEnumerator BoostEffect ()
    {
        ps.Play();
        psChild.Play();
        GetComponent<AudioSource>().Play();
        ChromaticAberration ChromAberr = null;
        PPV.profile.TryGetSettings(out ChromAberr);
        DepthOfField dop = null;
        PPV.profile.TryGetSettings(out dop);
        while (ChromAberr.intensity.value <= 1)
        {
            ChromAberr.intensity.value += (10f * Time.deltaTime);
            dop.focalLength.value += (40f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.4f);
        ps.Stop();
        psChild.Stop();
        
        while (ChromAberr.intensity.value >= 0)
        {
            ChromAberr.intensity.value -= (5 * Time.deltaTime);
            dop.focalLength.value -= (40f * Time.deltaTime);
            yield return null;
        }
        dop.focalLength.value = 260f;
    }
}
