using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireDisk : MonoBehaviour
{
    public float force;
    public float mineForce;
    public Image bg, fill;
    public Text text;
    public List<GameObject> discSelection = new List<GameObject>();
    public GameObject currentDisc;
    public GameObject raiderDisc;
    public GameObject mine;
    bool triggerDown = false, dpadTrigger = false, dpadLeft = false, dpadRight = false;
    float t;
    public float barSpeed;
    float power;
    public int currentI;
    ResourceHolder rh;
    ResourceCosts rc;
    PowerHolder ph;
    public PowerCosts pc;
    public Transform firingPoint;
    public Transform discFiringPoint;
    public Transform mineFiringPoint;

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
       // text.text = rc.resourcesID[currentI];
       /*
        if (Input.GetButtonDown("PadRB" + GetComponent<Health>().playerNum.ToString()) && !triggerDown)
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
        */

        if (Input.GetButtonDown("PadRB" + GetComponent<Health>().playerNum.ToString()))
        {
            //if (rh.resourceAmount >= rc.resourceCosts[currentI])
          //  {
                if (ph.powerAmount >= pc.powerCosts[2])
                {
                    GameObject Disc = Instantiate(raiderDisc, discFiringPoint.position, raiderDisc.transform.rotation);
                    Disc.GetComponent<CollisionDamage>().teamNum = GetComponent<Health>().playerNum;
                    Disc.GetComponent<Rigidbody>().AddForce(transform.forward * force);
                    if (Disc.GetComponent<ResourceCollection>() != null)
                        Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
                    if (Disc.GetComponent<Health>() != null)
                        Disc.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
                   // triggerDown = false;
                   // t = 0;
                   // power = 0;
                  //  fill.fillAmount = 0;
                  //  bg.gameObject.SetActive(false);
                   // rh.resourceAmount -= rc.resourceCosts[currentI];
                    ph.losePower(pc.powerCosts[2]);
                } else
                {
                    triggerDown = false;
                    t = 0;
                    power = 0;
                    fill.fillAmount = 0;
                    bg.gameObject.SetActive(false);
                }
          /*  } else
            {
                triggerDown = false;
                t = 0;
                power = 0;
                fill.fillAmount = 0;
                bg.gameObject.SetActive(false);
            } */
        }

        if (Input.GetButtonDown("PadLB" + GetComponent<Health>().playerNum.ToString()))
        {
            if (rh.resourceAmount >= rc.resourceCosts[4])
            {
                if (ph.powerAmount >= pc.powerCosts[3])
                {
                    GameObject Disc = Instantiate(mine, mineFiringPoint.position, mine.transform.rotation);
                    Disc.GetComponent<Rigidbody>().AddForce((-transform.forward + transform.up).normalized * mineForce);
                    Disc.GetComponent<MineTrigger>().teamNum = GetComponent<Health>().playerNum;
                    if (Disc.GetComponent<ResourceCollection>() != null)
                        Disc.GetComponent<ResourceCollection>().mbase = this.gameObject;
                    if (Disc.GetComponent<Health>() != null)
                        Disc.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
                    // triggerDown = false;
                    // t = 0;
                    // power = 0;
                    //  fill.fillAmount = 0;
                    //  bg.gameObject.SetActive(false);
                    rh.resourceAmount -= rc.resourceCosts[4];
                    ph.losePower(pc.powerCosts[3]);
                }
                else
                {
                    triggerDown = false;
                    t = 0;
                    power = 0;
                    fill.fillAmount = 0;
                    bg.gameObject.SetActive(false);
                }
            }
            else
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
