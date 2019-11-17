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

    private Vector3 flagOrigPos;
    public bool flagTaken = false;

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
            if (other.transform.GetComponent<Health>() != null)
            {
                if (other.transform.GetComponent<Health>().playerNum != playerNum)
                {
                    other.GetComponent<FlagHolder>().TakeFlag(flag);
                    flagTaken = true;
                }

                if (other.transform.GetComponent<Health>().playerNum == playerNum && other.transform.GetComponent<FlagHolder>().flagTaken == true)
                {
                    flagScore += 1;
                    fb.flagTaken = false;
                    other.GetComponent<FlagHolder>().DropFlag();
                    other.GetComponent<FlagHolder>().flag.GetComponent<Flag>().GoBackToStart();
                }
            }
        }
    }
}
