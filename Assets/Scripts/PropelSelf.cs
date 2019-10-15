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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RightTrigger" + GetComponent<Health>().playerNum.ToString()) > 0 && !triggerDown && !coolingDown)
        {
            triggerDown = true;
            bg.gameObject.SetActive(true);
        }

        if (triggerDown)
        {
            t += Time.deltaTime;
            power = Mathf.Pow((Mathf.Sin(t)), 2);
            fill.fillAmount = power;
        }

        if (Input.GetAxis("RightTrigger" + GetComponent<Health>().playerNum.ToString()) == 0 && triggerDown)
        {
            if (power < .25f) power = .25f;
            rb.AddForce(transform.forward * force * power);

            triggerDown = false;
            t = 0;
            power = 0;
            fill.fillAmount = 0;
            bg.gameObject.SetActive(false);
            coolingDown = true;
            StartCoroutine(Cooldown());
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
