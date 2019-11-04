using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHolder : MonoBehaviour
{

    public int resourceAmount = 30;
    public Text resourceText;
    

    // Start is called before the first frame update
    void Start()
    {
        //resourceText = GameObject.Find("ResourceCanvas").GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        resourceText.text = resourceAmount.ToString();
    }
}
