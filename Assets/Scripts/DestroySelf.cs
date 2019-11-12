using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroySequence()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
