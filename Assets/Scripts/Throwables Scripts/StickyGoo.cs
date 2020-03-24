using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyGoo : MonoBehaviour
{
    public float increasedDrag;
    public int teamNum;
    int numInside = 0;
    float sizeChangeFactor = -10f;
    public float lifeTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numInside > 0)
        {
            lifeTime -= Time.deltaTime;
            transform.localScale += new Vector3(0.1F, 0f, .1f) * sizeChangeFactor * Time.deltaTime;

            if (lifeTime <= 0f)
            {
                GetComponent<BoxCollider>().enabled = false;
                Destroy(this.gameObject, 1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() == true && other.GetComponent<Rigidbody>() == true && other.GetComponent<Health>().teamNum != teamNum)
        {
            numInside++;
            other.GetComponent<Rigidbody>().drag = increasedDrag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Health>() == true && other.GetComponent<Rigidbody>() == true && other.GetComponent<Health>().teamNum != teamNum)
        {
            numInside--;
            other.GetComponent<Rigidbody>().drag = 0f;
        }
    }
}
