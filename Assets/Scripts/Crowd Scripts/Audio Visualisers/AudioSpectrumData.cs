using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrumData : MonoBehaviour
{
    
    //public GameObject[][] crowdSections = new GameObject[10][];
    AudioSource aud;
    public float[] audSamples = new float[512];
    public Color col1;
    public List<Color> colors = new List<Color>();
    public float colorLerpTime = 10;
    [Range(1, 5)]
    public float intensity;



    // Use this for initialization
    void Start()
    {
        aud = GameObject.Find("MainGameMusic").GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrum();
    }

    void GetSpectrum()
    {
        aud.GetSpectrumData(audSamples, 0, FFTWindow.Blackman); // collects samples from the audio source
        for(int i = 0; i < 256; i++)
        {
            audSamples[i] *= 1500;
        }        
    }

    
}
