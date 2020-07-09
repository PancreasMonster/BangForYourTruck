using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFloorBeatVisualiser : MonoBehaviour
{
    public AudioSpectrumData ASD;
    public int bandNum;
    public float initialGlow;
    public float glowAmplitude;
    Material mat;
    public Color origMatColor, matColor;
    float baseColorValue;
    public float baseColorValueFadeSpeed;

    [Header("Colour Lerp")]
    public Color[] colors;

    public int currentIndex = 0;
    private int nextIndex;

    public float changeColourTime = 2.0f;

    private float lastChange = 0.0f;
    private float timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        KillManager.instance.gridFloors.Add(this);
        //to this object
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.onBeat.AddListener(onOnbeatDetected);
        processor.onSpectrum.AddListener(onSpectrum);

        nextIndex = (currentIndex + 1) % colors.Length;
    }
    // Update is called once per frame

    void onOnbeatDetected()
    {
        baseColorValue = 2;
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

        timer += Time.deltaTime;

        if (timer > changeColourTime)
        {
            currentIndex = (currentIndex + 1) % colors.Length;
            nextIndex = (currentIndex + 1) % colors.Length;
            timer = 0.0f;

        }
        origMatColor = Color.Lerp(colors[currentIndex], colors[nextIndex], timer / changeColourTime);

        if (baseColorValue > 0)
        {
            baseColorValue = Mathf.Lerp(baseColorValue, 0, baseColorValueFadeSpeed * Time.deltaTime);
        }
        //matColor = new Color(baseColorValue, baseColorValue, baseColorValue, origMatColor.a);
        mat.SetColor("_EmissionColor", origMatColor * (initialGlow + (glowAmplitude * baseColorValue)));
        
    }

    public void ChangeToTeamColour()
    {

    }

    IEnumerator TeamColourChange ()
    {
        yield return null;
    }
}
