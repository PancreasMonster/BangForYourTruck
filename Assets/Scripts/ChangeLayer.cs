using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LayerDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LayerDelay ()
    {
        yield return new WaitForSeconds(.5f);
        this.gameObject.layer = 0;
    }
}
