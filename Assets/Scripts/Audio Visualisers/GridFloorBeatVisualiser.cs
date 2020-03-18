using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFloorBeatVisualiser : MonoBehaviour
{
    public AudioSpectrumData ASD;
    public int bandNum;
    public float glowAmplitude;
    Material mat;
    public Color origMatColor, matColor;
    Texture2D t2d;
    public float baseColorValue;
    public float baseColorValueFadeSpeed;

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        origMatColor = GetComponent<Renderer>().material.color;

        //to this object
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.onBeat.AddListener(onOnbeatDetected);
        processor.onSpectrum.AddListener(onSpectrum);
    }
    // Update is called once per frame

    void onOnbeatDetected()
    {
        baseColorValue = 1;
        Debug.Log("Beat");
    }

    void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }

    void Update()
    {
        if(baseColorValue > 0)
        {
            baseColorValue = Mathf.Lerp(baseColorValue, 0, baseColorValueFadeSpeed * Time.deltaTime);
        }
        matColor = new Color(baseColorValue, baseColorValue, baseColorValue, origMatColor.a);
        mat.SetColor("_BaseColor", matColor);
        
    }
}
