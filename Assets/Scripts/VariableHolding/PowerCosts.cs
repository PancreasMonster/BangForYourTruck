using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCosts : MonoBehaviour
{
    public List<string> powerID = new List<string>();
    public List<int> powerCosts = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
