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
    Color col;


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
                
                col = Color.Lerp(col, ASD.colors[colorNum], ASD.colorLerpTime * Time.deltaTime);
               
    }
            else
            {

                col = Color.Lerp(col, ASD.col1, ASD.colorLerpTime * Time.deltaTime);

            }
            crowdSections[i].GetComponent<Renderer>().material.SetColor("Color_5DE2E5C", col);
        }
    }

    

    public void AssignBandNum(int num)
    {
        bandNum = num;
    }
}
