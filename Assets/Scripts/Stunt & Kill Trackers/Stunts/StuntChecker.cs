using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StuntChecker : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string flipSound;

    Transform tr;
    Rigidbody rb;
    FlipOver fo;
    Health h;
    MobilityCharges mc;
    public WheelSkid ws;
    public float driftIntensity = 2;
    public float stuntStringHoldTime = 1;
    float oldVelocity;

    [System.NonSerialized]
    public float score = 0;
    List<Stunt> stunts = new List<Stunt>();
    List<Stunt> doneStunts = new List<Stunt>();
    bool drifting;
    bool driftHold, jumpHold, flipHold;
    bool driftDisplay, jumpDisplay, flipDisplay;
    float driftDist;
    float driftScore;
    float endDriftTime;//Time during which drifting counts even if the vehicle is not actually drifting
    float jumpDist;
    float jumpTime;
    Vector3 jumpStart;

    public bool detectDrift = true;
    public bool detectJump = true;
    public bool detectFlips = true;

    public string driftString;//String indicating drift distance
    public string jumpString;//String indicating jump distance
    public string flipString;//String indicating flips
    [System.NonSerialized]
    public string stuntString;//String containing all stunts
    public string currentStunt;
    Vector3 localAngularVel;
    int teamNum;

    [Header("Concussive Flip")]
    public float concussiveRange;
    [Range(-1,1)]
    public float concussiveRadius;
    public float concussiveForce;
    public GameObject concussiveForcePS;
    public float concussiveForceDamage;
    public float maxScoreExplosion; //at what score does the explosion reach Max Damage;
    public int flipScore;
    bool landStunt = true;
    bool canSlam = true;
    public float randomAnnouncmentChance;

    Vector2 leftStick;
    float rightTrigger;
    float leftTrigger;
    float XButton;
    RaycastHit hit;
    Vector3 hitNormal = Vector3.up;
    public Animator anim;
    bool animCrash = false;

    private void OnLeftStick(InputValue value)
    {
        leftStick = value.Get<Vector2>();
    }

    private void OnRightTrigger(InputValue value)
    {
        rightTrigger = value.Get<float>();
    }

    private void OnLeftTrigger(InputValue value)
    {
        leftTrigger = value.Get<float>();
    }

    private void OnFaceButtonWest(InputValue value)
    {
        XButton = 1;
    }

    private void OnFaceButtonWestRelease(InputValue value)
    {
        XButton = 0;
    }

    void Start()
    {
        teamNum = GetComponent<Health>().teamNum;
        tr = transform;
        rb = GetComponent<Rigidbody>();
        fo = GetComponent<FlipOver>();
        h = GetComponent<Health>();
        mc = GetComponent<MobilityCharges>();
    }

    void FixedUpdate()
    {
        localAngularVel = tr.InverseTransformDirection(rb.angularVelocity);
        oldVelocity = Mathf.Abs(rb.velocity.y);
        //Detect drifts
        if (detectDrift && !fo.crashing)
        {
            DetectDrift();
        }
        else
        {
            drifting = false;
            driftDist = 0;
            driftScore = 0;
            driftString = "";
        }

        //Detect jumps
        if (detectJump && !fo.crashing)
        {
            //DetectJump();
        }
        else
        {
            jumpTime = 0;
            jumpDist = 0;
            jumpString = "";
        }

        //Detect flips
        if (detectFlips && !fo.crashing)
        {
            DetectFlips();
        }
        else
        {
            stunts.Clear();
            doneStunts.Clear();
            flipScore = 0;
            flipString = "";
        }

        //Combine strings into final stunt string
        stuntString = fo.crashing ? "Crashed" : driftString + jumpString + (string.IsNullOrEmpty(flipString) || string.IsNullOrEmpty(jumpString) ? "" : " + ") + flipString;

        if (!animCrash && fo.crashing)
        {
            animCrash = true;
            anim.SetBool("Crashed",true);
        }

        if(!fo.crashing)
        {
            animCrash = false;
        }
    }

    void DetectDrift()
    {
        endDriftTime = fo.timer < fo.timerAllowance ? ws.intensity > driftIntensity ? StuntManager.driftConnectDelayStatic : Mathf.Max(0, endDriftTime - Time.timeScale) : 0;
        drifting = endDriftTime > 0;
        Vector3 localVelocity = tr.InverseTransformDirection(rb.velocity);

        if (drifting && XButton > 0 && localVelocity.z > 20)
        {
            if(driftDisplay)
            {
                score += driftScore;
                driftDist = 0;
                driftScore = 0;
                driftString = "";
                driftDisplay = false;
            }
            driftHold = true;   
            driftDist += rb.velocity.magnitude * Time.fixedDeltaTime / 10;
            driftScore += driftDist / 100;
            driftString = "Drift: " + driftDist.ToString("n0") + " m ";

            /*  if (engine)
              {
                  engine.boost += (StuntManager.driftBoostAddStatic * Mathf.Abs(vp.localVelocity.x)) * Time.timeScale * 0.0002f;
              } */
        }      
        else if (!driftHold)
        {
            score += driftScore;
            driftDist = 0;
            driftScore = 0;
            driftString = "";
            
        }
        else
        {
            StartCoroutine(driftHoldActivation());
        }
    }

   /* void DetectJump()
    {
        if (fo.timer > fo.timerAllowance)
        {
            if(jumpDisplay)
            {
                score += (jumpDist + jumpTime) * StuntManager.jumpScoreRateStatic;

               

                jumpStart = tr.position;
                jumpDist = 0;
                jumpTime = 0;
                jumpString = "";
                jumpDisplay = false;
            }
            jumpHold = true;
            jumpDist = Vector3.Distance(jumpStart, tr.position) / 10;
            jumpTime += Time.fixedDeltaTime;
            jumpString = "Jump: " + jumpDist.ToString("n0") + " m";

         
        }
        else if (!jumpHold)
        {
            score += (jumpDist + jumpTime) * StuntManager.jumpScoreRateStatic;

           
            jumpStart = tr.position;
            jumpDist = 0;
            jumpTime = 0;
            jumpString = "";
        }
        else
        {
            StartCoroutine(jumpHoldActivation());
        }
    } */

    void DetectFlips()
    {
        if (fo.timer > fo.timerAllowance)
        {
            if(flipDisplay)
            {
                //Add stunt points to the score
                foreach (Stunt curStunt in stunts)
                {
                    // score += curStunt.score;
                    //  flipScore = Mathf.RoundToInt(score);
                    //flipScore += curStunt.score;

                }

                score += flipScore;
                stunts.Clear();
                doneStunts.Clear();
                flipString = "";
                flipDisplay = false;
            }

            if(!flipHold)
            flipScore = 0;
            flipHold = true;
            canSlam = true;
            landStunt = true;

            //Check to see if vehicle is performing a stunt and add it to the stunts list
            foreach (Stunt curStunt in StuntManager.stuntsStatic)
            {
                if (Vector3.Dot(localAngularVel.normalized, curStunt.rotationAxis) >= curStunt.precision)
                {
                    bool stuntExists = false;

                    foreach (Stunt checkStunt in stunts)
                    {
                        if (curStunt.name == checkStunt.name)
                        {
                           // flipScore += checkStunt.score;
                            stuntExists = true;
                            break;
                        }
                    }

                    if (!stuntExists)
                    {
                        
                        stunts.Add(new Stunt(curStunt));
                    }
                }
            }

            //Check the progress of stunts and compile the flip string listing all stunts
            foreach (Stunt curStunt2 in stunts)
            {
                if (Vector3.Dot(localAngularVel.normalized.normalized, curStunt2.rotationAxis) >= curStunt2.precision)
                {
                    curStunt2.progress += rb.angularVelocity.magnitude * Time.fixedDeltaTime;
                }

                if (curStunt2.progress * Mathf.Rad2Deg >= curStunt2.angleThreshold)
                {
                    bool stuntDoneExists = false;
                    

                    foreach (Stunt curDoneStunt in doneStunts)
                    {
                       
                        if (curDoneStunt == curStunt2)
                        {
                            
                            stuntDoneExists = true;
                            break;
                        }
                    }

                    if (!stuntDoneExists)
                    {
                        
                        currentStunt = curStunt2.name;
                        doneStunts.Add(curStunt2);
                    }
                }
            }

            string stuntCount = "";
            flipString = "";
            

            foreach (Stunt curDoneStunt2 in doneStunts)
            {
                stuntCount = curDoneStunt2.progress * Mathf.Rad2Deg >= curDoneStunt2.angleThreshold * 2 ? " x" + Mathf.FloorToInt((curDoneStunt2.progress * Mathf.Rad2Deg) / curDoneStunt2.angleThreshold).ToString() : "";
                flipString = string.IsNullOrEmpty(flipString) ? curDoneStunt2.name + stuntCount : flipString + " + " + curDoneStunt2.name + stuntCount;
               
            }
        }
        else if (!flipHold)
        {
            //Add stunt points to the score
            foreach (Stunt curStunt in doneStunts)
            {
                
                score += flipScore;
            }
            
            stunts.Clear();
            doneStunts.Clear();
            flipString = "";
        }
        else
        {
            StartCoroutine(flipHoldActivation());

            if (!fo.crashing && canSlam)
            {
                foreach (Stunt curStunt in doneStunts)
                {
                    flipScore += curStunt.score * Mathf.FloorToInt((curStunt.progress * Mathf.Rad2Deg) / curStunt.angleThreshold);
                }
                float rand = Random.Range(0f, 1f);             
                if (rand > randomAnnouncmentChance && flipScore >= maxScoreExplosion)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(flipSound, transform.position);                   
                }
                if(currentStunt != "")
                FlipConcussiveForce(flipScore);
               
                
            } 
            currentStunt = "";
        }
    }

    IEnumerator driftHoldActivation ()
    {
        driftDisplay = true;
        yield return new WaitForSeconds(stuntStringHoldTime);
        driftHold = false;
        driftDisplay = false;
    }

    IEnumerator jumpHoldActivation()
    {
        jumpDisplay = true;
        yield return new WaitForSeconds(stuntStringHoldTime);
        jumpHold = false;
        jumpDisplay = false;
        anim.SetBool("GoToScoreText",true);
    }

    IEnumerator flipHoldActivation()
    {
        flipDisplay = true;
        yield return new WaitForSeconds(stuntStringHoldTime);
        flipHold = false;
        flipDisplay = false;
        landStunt = true;
        anim.SetBool("GoToScoreText",true);
    }

    public void FlipEffect()
    {  
        //FlipConcussiveForce();   
    }

    private void FlipConcussiveForce (int currFlipScore)
    {
        canSlam = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, concussiveRange);
        List<Transform> targets = new List<Transform>();
        float slamDamage = concussiveForceDamage * (currFlipScore / maxScoreExplosion);
        if (currFlipScore > maxScoreExplosion)
            slamDamage = concussiveForceDamage;

        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject)
            {
                if (c.gameObject.GetComponent<Health>().teamNum != teamNum)
                {
                    targets.Add(c.transform);
                }
            }
        }

        foreach (Transform t in targets)
        {
            Vector3 dir = t.position - transform.position;
            dir.Normalize();
            //if (Vector3.Dot(transform.forward, dir) > concussiveRadius)
           // {
                Rigidbody rb = t.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(concussiveForce * Mathf.Min(1, currFlipScore / maxScoreExplosion) * rb.mass, transform.position, concussiveRange);

                Health h = t.GetComponent<Health>();
                if (h != null)
                    h.TakeDamage("Power Slammed", this.gameObject, concussiveForceDamage, Vector3.zero);
           // }
        }

        GameObject flipPS = Instantiate(concussiveForcePS, new Vector3 (transform.position.x, transform.position.y  - 4f, transform.position.z), concussiveForcePS.transform.rotation);
        Destroy(flipPS, 3);
        StartCoroutine(HelpLanding());
    }

    private void GainMobilityCharge ()
    {
        if(!mc.charge1)
        {
            mc.charge1Time = mc.rechargeTime;
            Debug.Log("mc1");
        } else if (!mc.charge2)
        {
            mc.charge2Time = mc.rechargeTime;
            Debug.Log("mc2");
        } else if (!mc.charge3)
        {
            mc.charge3Time = mc.rechargeTime;
            Debug.Log("mc3");
        }
    }

    public IEnumerator HelpLanding ()
    {
        float newVelocity = oldVelocity * .5f;
        
        float t = 0;
        rb.angularVelocity *= .25f;
        while (t < .25f)
        {
            rb.angularVelocity *= .25f;
            rb.velocity =  new Vector3(rb.velocity.x, rb.velocity.y *.6f, rb.velocity.z);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down, out hit, 7.5f, 14))
                hitNormal = hit.normal;

            Vector3 rotateAmount = Vector3.Cross(transform.up, hitNormal);
            rb.angularVelocity = rotateAmount;
            t += Time.deltaTime;
            
            rb.AddForce(transform.forward * newVelocity * Time.deltaTime / .25f, ForceMode.Acceleration);
            yield return null;
        }
        
        hitNormal = Vector3.up;
    }
}
