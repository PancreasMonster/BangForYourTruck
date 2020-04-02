using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    float DPADUpDown;
    
    private void OnDPADUpDown (InputValue value)
    {
        DPADUpDown = value.Get<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DPADUpDown < 0 && !dpadTrigger)
        {
            dpadTrigger = true;
            for(int i = 0; i < TH.currentTags; i++)
            {
                StartCoroutine(TagEject(i));
            }
            TH.currentTags = 0;
        }

      
        if (DPADUpDown == 0 && dpadTrigger)
        {           
            dpadTrigger = false;
        }
    }

    IEnumerator TagEject (int ejectTime)
    {
        TH.EmptyTags();
        yield return new WaitForSeconds(ejectTime/3f);
        GameObject Tag = Instantiate(tag, tagFiringPoint.position, tag.transform.rotation);
        Tag.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up).normalized * tagForce);
    }
}

