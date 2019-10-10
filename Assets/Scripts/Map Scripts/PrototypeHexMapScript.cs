using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrototypeHexMapScript : MonoBehaviour
{
    public List<int> sectionLenghts = new List<int>();
    public float width, height;
    public GameObject mapHex;
    int y;

    // Start is called before the first frame update
    void Start()
    {
        foreach (int i in sectionLenghts)
        {
            for (int x = 0; x < i; x++)
            {
               GameObject madeHex = Instantiate(mapHex, new Vector3((x * width) - ((i * width / 2f)-width/2f), 0, -(sectionLenghts.Count/2f*height)+(y*height)), mapHex.transform.rotation);
                madeHex.transform.name = "Hex0" + x.ToString() + " 0" + y.ToString();
            }
            y++;
        }

    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
