using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropelSelf : MonoBehaviour
{
    public float force;
    public Image bg, fill;
    Rigidbody rb;
    bool triggerDown = false;
    float t, power;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RightTrigger") > 0 && !triggerDown)
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

        if (Input.GetAxis("RightTrigger") == 0 && triggerDown)
        {
            rb.AddForce(transform.forward * force * power);
            triggerDown = false;
            t = 0;
            power = 0;
            fill.fillAmount = 0;
            bg.gameObject.SetActive(false);
        }
    }
}
