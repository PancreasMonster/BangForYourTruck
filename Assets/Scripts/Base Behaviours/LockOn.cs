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
    public Transform carFront;
    bool lockedOn;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            GameObject imageClone = Instantiate(targetImage, transform.position, Quaternion.identity);
            imageClone.transform.SetParent(targetImagesParent.transform, false);
            images.Add(imageClone.GetComponent<Image>());
        }
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
        if (Input.GetButtonDown("PadY" + GetComponent<Health>().playerNum.ToString()) && target == null)
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

        if (Input.GetButtonDown("PadY" + GetComponent<Health>().playerNum.ToString()) && target != null && lockedOn)
        {
            target = null;
            pivotCamera.target = null;
            /*foreach (Image i in images)
                {
                    i.gameObject.SetActive(false);
                }*/
            lockedOn = false;
        }

        if (target != null)
        {           
            float magDist = Vector3.Distance(target.transform.position, transform.position);
            if (magDist > 1500)
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

       ImageDisplay();

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

    void ImageDisplay ()
    {
        float UIdist = maxDistance;
        List<GameObject> UITargets = new List<GameObject>();
       
        
        for (int i = 0; i < targets.Count; i++)
        {
            images[i].color = Color.white;
            Vector3 screenPoint = cam.WorldToViewportPoint(targets[i].transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (onScreen)
            {
                images[i].rectTransform.position = cam.WorldToScreenPoint(new Vector3(targets[i].transform.position.x, targets[i].transform.position.y, targets[i].transform.position.z));
            } else
            {
                images[i].color = Color.white;
            }
            List<GameObject> detectedTargets = new List<GameObject>();


            if (target == null)
            {
                images[i].gameObject.SetActive(true);
                Vector3 dir = targets[i].transform.position - carFront.position;
                dir.Normalize();


                float UImagDist = Vector3.Distance(targets[i].transform.position, carFront.position);
                if (Vector3.Dot(carFront.forward, dir) > .2f && UImagDist <= UIdist)
                {
                    images[i].color = Color.red;
                    UITargets.Add(targets[i]);
                    UIdist = UImagDist;
                }
                else if (UImagDist <= maxDistance)
                {
                    images[i].color = Color.blue;
                }
                else
                {
                    images[i].color = Color.white;
                }
            } else
            {
                if (GameObject.ReferenceEquals(target, targets[i]))
                {
                    float UImagDist = Vector3.Distance(targets[i].transform.position, carFront.position);
                    if (UImagDist < 50)
                    {
                        images[i].color = Color.green;
                    } else
                    {
                        images[i].color = Color.yellow;
                    }

                    
                } else
                {
                    images[i].gameObject.SetActive(false);
                }
            }

            /* if (i == targets.Count - 1)
             {
                 foreach(Image iImage in images)
                 {
                     if (UImagDist >= maxDistance)
                     {
                         images[i].color = Color.blue;
                     }
                     else
                     {

                     }
                 }
                 UITargets.Clear();
                 UIdist = maxDistance;
             } */




            //float dist = maxDistance;

            //    float magDist = Vector3.Distance(t.transform.position, transform.position);
            /* if (magDist < dist)
             {
                 target = t;
                 pivotCamera.target = t;                       
                 dist = magDist;
                 */

          
        }

            for (int x = 0; x < UITargets.Count; x++)
            {
                float UImagDist = Vector3.Distance(UITargets[x].transform.position, carFront.position);
          
                if (UImagDist > UIdist && UImagDist <= maxDistance)
                {
                    images[x].color = Color.blue;
                }
                else
                {
                    images[x].color = Color.red;
                }


            } 

           /* for (int y = 0; y < targets.Count; y++)
            {
                Vector3 dir = targets[y].transform.position - transform.position;
                dir.Normalize();


                float UImagDist = Vector3.Distance(targets[y].transform.position, transform.position);
                if (Vector3.Dot(transform.forward, dir) < .2f)
                {
                    images[y].color = Color.blue;                 
                }

                
            } */

           
           


        
    }

    IEnumerator targetAcquire ()
    {
        yield return null;
        lockedOn = true;
    }
}
