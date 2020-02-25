using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickEjectTags : MonoBehaviour
{
    TagHolder TH;
    bool dpadTrigger;
    public GameObject tag;
    public Transform tagFiringPoint;
    public float tagForce = 200;

    // Start is called before the first frame update
    void Start()
    {
        TH = GetComponent<TagHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("DPADVertical" + GetComponent<Health>().playerNum.ToString()) < 0 && !dpadTrigger)
        {
            dpadTrigger = true;
            for(int i = 0; i < TH.currentTags; i++)
            {
                StartCoroutine(TagEject(i));
            }
            TH.currentTags = 0;
        }

      
        if (Input.GetAxis("DPADVertical" + GetComponent<Health>().playerNum.ToString()) == 0 && dpadTrigger)
        {           
            dpadTrigger = false;
        }
    }

    IEnumerator TagEject (int ejectTime)
    {
        yield return new WaitForSeconds(ejectTime/3f);
        GameObject Tag = Instantiate(tag, tagFiringPoint.position, tag.transform.rotation);
        Tag.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up).normalized * tagForce);
    }
}

