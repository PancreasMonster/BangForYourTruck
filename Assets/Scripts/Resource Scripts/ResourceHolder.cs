using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHolder : MonoBehaviour
{

    public int resourceAmount = 30;
    public int resourceIncome;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PassiveIncome());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    IEnumerator PassiveIncome ()
    {
        yield return new WaitForSeconds(5);
        resourceAmount += resourceIncome;
        StartCoroutine(PassiveIncome());
    }
}
