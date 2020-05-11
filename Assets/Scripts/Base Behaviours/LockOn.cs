using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LockOn : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject target;
    public GameObject targetImagesParent;
    public GameObject targetImage;
    public List<Image> images = new List<Image>();
    public Camera cam;
    public float maxDistance = 100f;
    public Orbit camTarget;
    public FollowObject pivotCamera;
    GameObject targetImageGO;
    public Transform carFront;
    bool lockedOn;
    Color origCol, origColNoA;

    // Start is called before the first frame update
    void Start()
    {
            GameObject imageClone = Instantiate(targetImage, transform.position, Quaternion.identity);
            imageClone.transform.SetParent(targetImagesParent.transform, false);
            images.Add(imageClone.GetComponent<Image>());
            origCol = images[0].color;
            origColNoA = new Color(0, 0, 0, 0);
    }

    private void OnFaceButtonNorth (InputValue value)
    {
        if (target == null)
        {
            // Debug.Log("Found");
            List<GameObject> detectedTargets = new List<GameObject>();
            foreach (GameObject t in targets)
            {
                Vector3 dir = t.transform.position - carFront.position;
                dir.Normalize();
                //Debug.Log(Vector3.Dot(transform.forward, dir));
                if (Vector3.Dot(carFront.forward, dir) > .2f)
                {
                    detectedTargets.Add(t);
                    //   Debug.Log(t.transform.name);
                }
            }

            float dist = maxDistance;
            foreach (GameObject t in detectedTargets)
            {
                float magDist = Vector3.Distance(t.transform.position, carFront.position);
                if (magDist < dist)
                {
                    target = t;
                    pivotCamera.target = t;
                    dist = magDist;
                    StartCoroutine(targetAcquire());
                }
            }
        }

        if (target != null && lockedOn)
        {
            target = null;
            pivotCamera.target = null;
            /*foreach (Image i in images)
                {
                    i.gameObject.SetActive(false);
                }*/
            lockedOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {       
        ImageDisplay();
    }

    void ImageDisplay ()
    {

        if (target != null)
        {
            images[0].color = origColNoA;
            return;
            
        }

            List<GameObject> detectedTargets2 = new List<GameObject>();
        foreach (GameObject t in targets)
        {
            Vector3 dir = t.transform.position - carFront.position;
            dir.Normalize();
            //Debug.Log(Vector3.Dot(transform.forward, dir));
            if (Vector3.Dot(carFront.forward, dir) > .2f)
            {
                detectedTargets2.Add(t);
                //   Debug.Log(t.transform.name);
            }
        }

        
        float dist2 = maxDistance;
        int listSize2 = 0;
        foreach (GameObject t in detectedTargets2)
        {
            float magDist = Vector3.Distance(t.transform.position, carFront.position);
            if (magDist < dist2)
            {
                targetImageGO = t;
                dist2 = magDist;
                listSize2++;
            }
        }

        if (listSize2 == 0)
        {
            targetImageGO = null;
            images[0].color = origColNoA;
        }

        if (targetImageGO)
        {
            images[0].rectTransform.position = cam.WorldToScreenPoint(new Vector3(targetImageGO.transform.position.x, targetImageGO.transform.position.y, targetImageGO.transform.position.z));
            images[0].color = origCol;
        }

        

           
           


        
    }

    IEnumerator targetAcquire ()
    {
        yield return null;
        lockedOn = true;
    }
}
