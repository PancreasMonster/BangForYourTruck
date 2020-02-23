using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildModeFire : MonoBehaviour
{
    LineRenderer lr;
    public Image bg, fill;
    public Text text;
    public List<GameObject> discSelection = new List<GameObject>();
    public Image image;
    public List<Sprite> icons = new List<Sprite>();
    public GameObject currentDisc;
    bool triggerDown = false, dpadTrigger = false, dpadLeft = false, dpadRight = false;
    public float velocity;
    public float fireAngle;
    public float mortarSpeed;
    public Transform aimTarget, firingPoint;
    public GameObject bomb;
    public int currentI;
    private bool cooldown;
    public float cooldownDelay = 1.5f;
    ResourceHolder rh;
    ResourceCosts rc;
    PowerHolder ph;
    public PowerCosts pc;
    [Range (1, 50)]
    public int resolution;

    float g;
    float radianAngle;

     void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        currentDisc = discSelection[0];
        rh = GetComponent<ResourceHolder>();
        rc = GameObject.Find("ResourceCost").GetComponent<ResourceCosts>();
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
       // text.text = rc.resourcesID[currentI];
      //  image.sprite = icons[currentI];

        RenderArc();
        FindVelocity(aimTarget, fireAngle);
        if (Input.GetButtonUp("PadRB" + GetComponent<Health>().playerNum.ToString()) && !cooldown)
        {
            if (rh.resourceAmount >= rc.resourceCosts[currentI])
            {
                if (ph.powerAmount >= pc.powerCosts[1])
                {
                    cooldown = true;
                    StartCoroutine(Cooldown());
                    GameObject clone = Instantiate(currentDisc, firingPoint.position, Quaternion.identity);
                    if (clone.GetComponent<ResourceCollection>() != null)
                        clone.GetComponent<ResourceCollection>().mbase = this.gameObject;
                    if (clone.GetComponent<Health>() != null)
                        clone.GetComponent<Health>().playerNum = GetComponent<Health>().playerNum;
                    if (clone.GetComponent<Bomb>() != null)
                    {
                        StopAllCoroutines();
                        cooldown = false;
                    }
                        
                    Rigidbody unitRB = clone.GetComponent<Rigidbody>();
                    unitRB.velocity = BallisticVel(aimTarget, fireAngle);
                    rh.resourceAmount -= rc.resourceCosts[currentI];
                    ph.losePower(pc.powerCosts[1]);
                }
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
                if (currentI == 0)
                {
                    currentI = discSelection.Count - 1;
                    currentDisc = discSelection[currentI];
                    dpadLeft = false;
                }
                else
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

    // Renders the trajectory path of projectile
    void RenderArc ()
    {
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArc());
    }

    Vector3[] CalculateArc()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * fireAngle;
        float maxDistance = (velocity * velocity * Mathf.Sin (2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    //calculates each point of the array 
    Vector3 CalculateArcPoint (float t, float maxDistance)
    {
        float z = t * maxDistance;
        float y = z * Mathf.Tan(radianAngle) - ((g * z * z) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        Vector3 arcRot = new Vector3(0, y, z);
        transform.TransformPoint(arcRot);
        return arcRot;
    }

    public Vector3 BallisticVel(Transform target, float angle)
    {
        Vector3 targetDir = target.position - firingPoint.position;
        float dist = targetDir.magnitude;
        float radAngle = angle * Mathf.Deg2Rad;
        targetDir.y = dist * Mathf.Tan(radAngle);
        float fireVelocity = Mathf.Sqrt(dist * Physics.gravity.magnitude * mortarSpeed / Mathf.Sin(radAngle * 2));
        return fireVelocity * targetDir.normalized;
    }

    public void FindVelocity (Transform target, float angle)
    {
        Vector3 targetDir = target.position - firingPoint.position;
        float dist = targetDir.magnitude;
        float radAngle = angle * Mathf.Deg2Rad;
        targetDir.y = dist * Mathf.Tan(radAngle);
        velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude * mortarSpeed / Mathf.Sin(radAngle * 2));
    }

    IEnumerator Cooldown ()
    {
        yield return new WaitForSeconds(cooldownDelay);
        cooldown = false;
    }
}
