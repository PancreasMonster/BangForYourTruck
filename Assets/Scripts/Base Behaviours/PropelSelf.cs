using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropelSelf : MonoBehaviour
{
    public float force;
    public Image bg, fill, bgcd, fillcd;  //cd = cooldown
    public float cooldownDelay;    Rigidbody rb;
    bool triggerDown = false, coolingDown = false;
    float t, power;
    PowerHolder ph;
    PowerCosts pc;
    RaycastHit hit;
    LayerMask layer;

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

        if (Physics.Raycast(transform.position, Vector3.down * 5, out hit, 5, layer))
        {
            if (Input.GetButtonDown("PadA" + GetComponent<Health>().playerNum.ToString()) /*&& triggerDown*/)
            {
                if (ph.powerAmount >= pc.powerCosts[0])
                {
                   
                    rb.AddForce(transform.forward * force /* * power */);

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
        } else
        {
            if (Input.GetButtonDown("PadA" + GetComponent<Health>().playerNum.ToString()) /*&& triggerDown*/)
            {
                if (ph.powerAmount >= pc.powerCosts[0])
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(transform.forward * force /* * power */);

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
}
