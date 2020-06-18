using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSpectrumData : MonoBehaviour
{
    
    //public GameObject[][] crowdSections = new GameObject[10][];
    public AudioSource aud;
    public float[] audSamples = new float[512];
    public Color col1;
    public List<Color> colors = new List<Color>();
    public float colorLerpTime = 10;
    [Range(1, 5)]
    public float intensity;
    public AudioMixer audMix;
    public GameObject audioVisualiser;
    public bool MainMenu = false;


    // Use this for initialization
    void Start()
    {
        aud = ScenesManager.instance.gameMusic;

        if (PlayerPrefs.GetInt("AVOn") == 1)
        {
            audioVisualiser.SetActive(false);
        }
        else
        {
            audioVisualiser.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrum();
        if (audMix)
        {
            audMix.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicMixer"));
            audMix.SetFloat("Master", PlayerPrefs.GetFloat("MasterMixer"));
            audMix.SetFloat("SFX", PlayerPrefs.GetFloat("SFXMixer"));
        }

        if (audioVisualiser && MainMenu)
        {
            if (PlayerPrefs.GetInt("AVOn") == 1)
            {
                audioVisualiser.SetActive(false);
            }
            else
            {
                audioVisualiser.SetActive(true);
            }
        }
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
