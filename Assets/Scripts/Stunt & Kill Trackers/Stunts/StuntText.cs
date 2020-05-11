using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuntText : MonoBehaviour
{
    Text text;
    public StuntChecker sc;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Debug.Log(transform.name);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = sc.stuntString;
    }
}
