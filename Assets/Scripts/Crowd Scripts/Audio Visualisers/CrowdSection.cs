using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrowdSection : MonoBehaviour
{
    public List<GameObject> crowdSections = new List<GameObject>();

    public AudioSpectrumData ASD;
    public int bandNum;
    public int colorNum = 0;

    // Use this for initialization
    void Start()
    {
        //ASD = GameObject.FindGameObjectWithTag("StadiumSeparatedCrowd").GetComponent<AudioSpectrumData>();
         foreach (Transform child in transform)
         {
             crowdSections.Add(child.transform.gameObject);
         }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < crowdSections.Count; i++)
        {
            if (((float)(i + 1 / crowdSections.Count)) < ((float)ASD.audSamples[bandNum]))
            {
                
                crowdSections[i].GetComponent<Renderer>().material.SetColor("Color_5DE2E5C", ASD.colors[colorNum]);
               
    }
            else
            {
               
                crowdSections[i].GetComponent<Renderer>().material.SetColor("Color_5DE2E5C", ASD.col1);

            }
        }
    }

    

    public void AssignBandNum(int num)
    {
        bandNum = num;
    }
}
