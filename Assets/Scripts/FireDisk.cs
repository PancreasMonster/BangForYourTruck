using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireDisk : MonoBehaviour
{
    public float force;
    public Image bg, fill;
    public GameObject disc;
    bool triggerDown = false;
    float t, power;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("LeftTrigger" + GetComponent<Health>().playerNum) > 0 && !triggerDown)
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

        if (Input.GetAxis("LeftTrigger" + GetComponent<Health>().playerNum) == 0 && triggerDown)
        {
            GameObject Disc = Instantiate(disc, transform.position, Quaternion.identity);
            Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force * power);
            Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
            triggerDown = false;
            t = 0;
            power = 0;
            fill.fillAmount = 0;
            bg.gameObject.SetActive(false);
        }
    }
}
