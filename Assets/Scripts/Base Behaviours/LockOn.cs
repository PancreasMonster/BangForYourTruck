using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOn : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject target;
    public Image image;
    public Camera cam;
    public float maxDistance = 100f;
    public Orbit camTarget;
    public FollowObject pivotCamera;
    bool lockedOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PadLB" + GetComponent<Health>().playerNum.ToString()) && target == null)
        {
           // Debug.Log("Found");
            List<GameObject> detectedTargets = new List<GameObject>();
            foreach (GameObject t in targets)
            {
                Vector3 dir = t.transform.position - transform.position;
                dir.Normalize();
                //Debug.Log(Vector3.Dot(transform.forward, dir));
                if (Vector3.Dot(transform.forward, dir) > 0)
                {
                    detectedTargets.Add(t);
                 //   Debug.Log(t.transform.name);
                }
            }

            float dist = maxDistance;
            foreach (GameObject t in detectedTargets)
            {
                float magDist = Vector3.Distance(t.transform.position, transform.position);
                if (magDist < dist)
                {
                    target = t;
                    pivotCamera.target = t;
                    image.gameObject.SetActive(true);
                    dist = magDist;
                    StartCoroutine(targetAcquire());
                }
            }
        }

        if (Input.GetButtonDown("PadLB" + GetComponent<Health>().playerNum.ToString()) && target != null && lockedOn)
        {
            target = null;
            pivotCamera.target = null;
            image.gameObject.SetActive(false);
            lockedOn = false;
        }

        if (target != null)
        {
            LockedOn();
            float magDist = Vector3.Distance(target.transform.position, transform.position);
            if (magDist > 1500)
            {
                target = null;
                pivotCamera.target = null;
                image.gameObject.SetActive(false);
                lockedOn = false;
            }
        }

        /*if (target != null)
        {
            LockedOn();
            Vector3 dir = target.transform.position - transform.position;
            float magDist = Vector3.Distance(target.transform.position, transform.position);
            if (Vector3.Dot(transform.forward, dir) < 0 || magDist > maxDistance)
            {
                target = null;
                image.gameObject.SetActive(false);
                lockedOn = false;
            }
        }*/
    }

    void LockedOn ()
    {
        image.rectTransform.position = cam.WorldToScreenPoint(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));
    }

    IEnumerator targetAcquire ()
    {
        yield return null;
        lockedOn = true;
    }
}
