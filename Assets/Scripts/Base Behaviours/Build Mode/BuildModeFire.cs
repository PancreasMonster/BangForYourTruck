using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildModeFire : MonoBehaviour
{
    
    public Text text;
    public List<GameObject> discSelection = new List<GameObject>();
    public List<int> ammo = new List<int>();
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
    LineRenderer lr;
    float g;
    float radianAngle;
    Vector3 targetOriginalPos;
    public float targetZDistance;
    public float maxTargetDistance = 6;
    public float targetMovespeed = 3;
    public float turnSpeed = 270;
    [Header("RangeSelection")]
    public List<float> ranges = new List<float>();
    public int currentRange = 1;
    public bool forwardRange = true, backwardRange = true;
    public float rangeDelay;



    void Start()
    {
        currentDisc = discSelection[0];       
        ph = GetComponent<PowerHolder>();
        pc = GameObject.Find("PowerCost").GetComponent<PowerCosts>();
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
        targetOriginalPos = aimTarget.transform.localPosition;
    }

    Vector2 rightStick;
    float PadLB;
    float PadRB;
    float DPadLeftRight;

    private void OnRightStick(InputValue value)
    {
        rightStick = value.Get<Vector2>();
    }

    private void OnLeftBumper(InputValue value)
    {
        PadLB = 1;
        if (!cooldown)
        {
            aimTarget.transform.localPosition = targetOriginalPos;
        }
    }

    private void OnRightBumper(InputValue value)
    {
        PadRB = 1;
    }

    private void OnLeftBumperRelease(InputValue value)
    {
        PadLB = 0;
    }

    private void OnRightBumperRelease(InputValue value)
    {
        PadRB = 0;
    }

    private void OnDPADLeftRight(InputValue value)
    {
        DPadLeftRight = value.Get<float>();
        Debug.Log(DPadLeftRight);
    }


    // Update is called once per frame
    void Update()
    {
        // text.text = rc.resourcesID[currentI];
        //  image.sprite = icons[currentI];
        //if (Input.GetButtonDown("PadLB" + GetComponent<Health>().playerNum.ToString()) && !cooldown)
     //   {
     //       aimTarget.transform.localPosition = targetOriginalPos;
      //  }  

        if (PadLB > 0 && !cooldown)
        {
         
            RenderArc();
            targetZDistance = ranges[currentRange];

            if(rightStick.y < 0 && currentRange < ranges.Count - 1 && forwardRange)
            {
                backwardRange = true;
                forwardRange = false;
                currentRange++;

                StartCoroutine(resetRangeBools(0));
            }
            else if (rightStick.y > 0 && currentRange > 0 && backwardRange)
            {
                forwardRange = true;
                backwardRange = false;
                currentRange--;

                StartCoroutine(resetRangeBools(1));
            }
            aimTarget.localPosition = new Vector3(aimTarget.transform.localPosition.x, aimTarget.transform.localPosition.y, targetOriginalPos.z + targetZDistance);
        }

        FindVelocity(aimTarget, fireAngle);
        if (PadLB == 0 && !cooldown)
        {
            targetZDistance = 0;
            lr.positionCount = 0;
           /* if (ammo[currentI] > 0)
            {
                cooldown = true;
                StartCoroutine(Cooldown());
                GameObject clone = Instantiate(currentDisc, firingPoint.position, Quaternion.identity);
                clone.transform.rotation = Quaternion.Lerp(clone.transform.rotation, transform.rotation, 1);
                if (clone.GetComponent<ResourceCollection>() != null)
                    clone.GetComponent<ResourceCollection>().mbase = this.gameObject;
                if (clone.GetComponent<Health>() != null)
                    clone.GetComponent<Health>().teamNum = GetComponent<Health>().teamNum;
                if (clone.GetComponent<Bomb>() != null)
                {
                   // StopAllCoroutines();
                    cooldown = false;
                }

                Rigidbody unitRB = clone.GetComponent<Rigidbody>();
                unitRB.velocity = BallisticVel(aimTarget, fireAngle);
                ammo[currentI]--;
                
            }*/
        }

        if (PadLB > 0 && PadRB > 0 && !cooldown)
        {
            targetZDistance = 0;
            lr.positionCount = 0;
            if (ammo[currentI] > 0)
            {
                cooldown = true;
                StartCoroutine(Cooldown());
                GameObject clone = Instantiate(currentDisc, firingPoint.position, transform.rotation);
                // clone.transform.rotation = Quaternion.Lerp(clone.transform.rotation, transform.rotation, 1);
                if (clone.transform.name == "BlueLaserTurret" || clone.transform.name == "RedLaserTurret" || clone.transform.name == "BlueParticleTurret" || clone.transform.name == "RedParticleTurret")
                    clone.transform.rotation = Quaternion.Euler(-90, clone.transform.rotation.eulerAngles.x, clone.transform.rotation.eulerAngles.z);
                if (clone.GetComponent<ResourceCollection>() != null)
                    clone.GetComponent<ResourceCollection>().mbase = this.gameObject;
                if (clone.GetComponent<Health>() != null)
                    clone.GetComponent<Health>().teamNum = GetComponent<Health>().teamNum;
                if (clone.GetComponent<Bomb>() != null)
                {                 
                    cooldown = false;
                }
                if (clone.GetComponent<Missile>() != null)
                    clone.GetComponent<Missile>().teamNum = GetComponent<Health>().teamNum;

                Rigidbody unitRB = clone.GetComponent<Rigidbody>();
                unitRB.velocity = BallisticVel(aimTarget, fireAngle);
                ammo[currentI]--;
            }
        }

        if (DPadLeftRight < 0 && !dpadTrigger)
        {
            dpadTrigger = true;
            dpadLeft = true;
        }

        if (DPadLeftRight > 0 && !dpadTrigger)
        {
            dpadTrigger = true;
            dpadRight = true;
        }

        if (DPadLeftRight == 0 && dpadTrigger)
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
        if (targetZDistance >= 0)
        {
            float z = t * maxDistance;
            float y = z * Mathf.Tan(radianAngle) - ((g * z * z) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
            Vector3 arcRot = new Vector3(0, y, z);
            transform.TransformPoint(arcRot);
            return arcRot;
        } else
        {
            float z = t * maxDistance;
            float y = z * Mathf.Tan(radianAngle) - ((g * z * z) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
            Vector3 arcRot = new Vector3(0, y, -z);
            transform.TransformPoint(arcRot);
            return arcRot;
        }
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

    IEnumerator resetRangeBools (int i)
    {
        if (i == 0)
        {
            forwardRange = false;
            yield return new WaitForSeconds(rangeDelay);
            forwardRange = true;
        } else
        {
            backwardRange = false;
            yield return new WaitForSeconds(rangeDelay);
            backwardRange = true;
        }
    }
}
