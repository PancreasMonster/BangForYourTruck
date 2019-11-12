using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireDisk : MonoBehaviour
{
    public float force;
    public Image bg, fill;
    public Text text;
    public List<GameObject> discSelection = new List<GameObject>();
    public GameObject currentDisc;
    bool triggerDown = false, dpadTrigger = false, dpadLeft = false, dpadRight = false;
    float t;
    public float barSpeed;
    float power;
    public int currentI;
    ResourceHolder rh;
    ResourceCosts rc;
    PowerHolder ph;
    public PowerCosts pc;

    // Start is called before the first frame update
    void Start()
    {
        currentDisc = discSelection[0];
        rh = GetComponent<ResourceHolder>();
        rc = GameObject.Find("ResourceCost").GetComponent<ResourceCosts>();
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = rc.resourcesID[currentI];

        if (Input.GetButtonDown("PadX" + GetComponent<Health>().playerNum.ToString()) && !triggerDown)
        {
            triggerDown = true;
            bg.gameObject.SetActive(true);
        }

        if (triggerDown)
        {
            t += Time.deltaTime * barSpeed;
            power = Mathf.Pow((Mathf.Sin(t)), 2);
            fill.fillAmount = power;
        }

        if (Input.GetButtonUp("PadX" + GetComponent<Health>().playerNum.ToString()) && triggerDown)
        {
            if (rh.resourceAmount >= rc.resourceCosts[currentI])
            {
                if (ph.powerAmount >= pc.powerCosts[1])
                {
                    GameObject Disc = Instantiate(currentDisc, transform.position + new Vector3(0, 0, 0), currentDisc.transform.rotation);
                    Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force * power);
                    if (Disc.GetComponent<ResourceCollection>() != null)
                        Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
                    if (Disc.GetComponent<Health>() != null)
                        Disc.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
                    triggerDown = false;
                    t = 0;
                    power = 0;
                    fill.fillAmount = 0;
                    bg.gameObject.SetActive(false);
                    rh.resourceAmount -= rc.resourceCosts[currentI];
                    ph.losePower(pc.powerCosts[1]);
                } else
                {
                    triggerDown = false;
                    t = 0;
                    power = 0;
                    fill.fillAmount = 0;
                    bg.gameObject.SetActive(false);
                }
            } else
            {
                triggerDown = false;
                t = 0;
                power = 0;
                fill.fillAmount = 0;
                bg.gameObject.SetActive(false);
            }
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
