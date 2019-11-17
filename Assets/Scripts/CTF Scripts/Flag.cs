using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Vector3 origPos;
    public float waitTime = 10;
    private bool flagTaken;
    private bool dropped;
    private float timer = 0;
    public FlagBase fb;

    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.localPosition;  
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 && dropped)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0)
        {
            transform.localPosition = origPos;
            fb.flagTaken = false;
            dropped = false;
        }
    }

    public void GoBackToStart()
    {
        transform.localPosition = origPos;
        flagTaken = false;
    }

    public void StartTimer()
    {
        timer = waitTime;
        dropped = true;
        StartCoroutine(changeLayer());
    }

    public void FallOff()
    {
        transform.localPosition = origPos;
        fb.flagTaken = false;
    }

    public void setFlagTaken(string state)
    {
        if (state == "true")
        {
            flagTaken = true;
        } else
        {
            flagTaken = false;
        }
    }

    public void setDropped(string state)
    {
        if (state == "true")
        {
            dropped = true;
        }
        else
        {
            dropped = false;
        }
    }

    IEnumerator changeLayer()
    {
        yield return new WaitForSeconds(.25f);
        transform.gameObject.layer = 0;
        Transform[] childrenT = GetComponentsInChildren<Transform>();
        foreach(Transform t in childrenT)
        {
            t.gameObject.layer = 0;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.layer == 8)
        {
            dropped = false;
            coll.transform.GetComponent<FlagHolder>().TakeFlag(this.gameObject);
            transform.gameObject.layer = 15;
            Transform[] childrenT = GetComponentsInChildren<Transform>();
            foreach (Transform t in childrenT)
            {
                t.gameObject.layer = 15;
            }
        }
    }
}
