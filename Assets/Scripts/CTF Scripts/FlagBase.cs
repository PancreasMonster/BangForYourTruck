using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagBase : MonoBehaviour
{
    public int playerNum;
    public GameObject flag;
    public int flagScore;
    public FlagBase fb;

    public Vector3 flagOrigPos;
    public int hitCount;
    public bool flagTaken = false, hitDetect, flagDrop;

    // Start is called before the first frame update
    void Start()
    {
        flagOrigPos = flag.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!flagTaken)
        {
            if (other.transform.GetComponentInParent<Health>() != null)
            {
                if (other.transform.GetComponentInParent<Health>().playerNum != playerNum)
                {
                    if (hitDetect == false)
                    {
                        if (!flagDrop)
                        {
                            other.GetComponentInParent<FlagHolder>().TakeFlag(flag);
                            flagTaken = true;
                            hitDetect = true;
                            hitCount += 1;
                            Debug.Log("Hit" + hitCount.ToString());
                        }
                    }
                }

                if (other.transform.GetComponentInParent<Health>().playerNum == playerNum && other.transform.GetComponentInParent<FlagHolder>().flagTaken == true)
                {
                    flagScore += 1;
                    fb.flagTaken = false;
                    fb.hitDetect = false;
                    fb.flagDrop = false;
                    other.GetComponentInParent<FlagHolder>().DeliverFlag();
                    other.GetComponentInParent<FlagHolder>().flag.GetComponent<Flag>().GoBackToStart();
                }
            }
        }
    }
}
