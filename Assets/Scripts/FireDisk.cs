using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireDisk : MonoBehaviour
{
    public float force;
    public Image bg, fill;
    public List<GameObject> discSelection = new List<GameObject>();
    public GameObject currentDisc;
    bool triggerDown = false, dpadTrigger = false, dpadLeft = false, dpadRight = false;
    float t, power;
    public int currentI;

    // Start is called before the first frame update
    void Start()
    {
        currentDisc = discSelection[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("LeftTrigger" + GetComponent<Health>().playerNum.ToString()) > 0 && !triggerDown)
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

        if (Input.GetAxis("LeftTrigger" + GetComponent<Health>().playerNum.ToString()) == 0 && triggerDown)
        {
            GameObject Disc = Instantiate(currentDisc, transform.position, Quaternion.identity);
            Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force * power);
            if(Disc.GetComponent<ResourceCollection>() != null)
            Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
            triggerDown = false;
            t = 0;
            power = 0;
            fill.fillAmount = 0;
            bg.gameObject.SetActive(false);
        }

        if (Input.GetAxis("DPADHorizontal" + GetComponent<Health>().playerNum.ToString()) < 0 && !dpadTrigger)
        {
            dpadTrigger = true;
            dpadLeft = true;
        }

        if (Input.GetAxis("DPADHorizontal" + GetComponent<Health>().playerNum.ToString()) > 0 && !dpadTrigger)
        {
            dpadTrigger = true;
            dpadRight = true;
        }

        if (Input.GetAxis("DPADHorizontal" + GetComponent<Health>().playerNum.ToString()) == 0 && dpadTrigger)
        {
            if (dpadLeft)
            {
                if(currentI == 0)
                {
                    currentI = discSelection.Count - 1;
                    currentDisc = discSelection[currentI];
                    dpadLeft = false;
                } else
                {
                    currentI--;
                    currentDisc = discSelection[currentI];
                    dpadLeft = false;
                }
            }

            if (dpadRight)
            {
                if (currentI == discSelection.Count - 1)
                {
                    currentI = 0;
                    currentDisc = discSelection[currentI];
                    dpadRight = false;
                }
                else
                {
                    currentI++;
                    currentDisc = discSelection[currentI];
                    dpadRight = false;
                }
            }
            dpadTrigger = false;
        }


    }
}
