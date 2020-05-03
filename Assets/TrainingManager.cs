using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TrainingManager : MonoBehaviour
{
    public int trainingStage;
    GameObject trainingCanvas;

    public float textDelay;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        Training();
        trainingCanvas = GameObject.Find("Training Canvas");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProceedTraining()
    {
        trainingStage++;
        Training();
        audio.Play();
    }

    void Training()
    {
        if (trainingStage == 0)
        {
            Debug.Log(trainingStage);
            textDelay = 2f;
            Invoke("DisplayText", 1f);
            Invoke("ProceedTraining", 2f);
        }

        if (trainingStage == 1)
        {
            Debug.Log(trainingStage);

            trainingCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (trainingStage == 2)
        {
            Debug.Log(trainingStage);

            textDelay = 3f;

        }

        if (trainingStage == 3)
        {
            Debug.Log(trainingStage);

            textDelay = 2f;

        }

        if (trainingStage == 4)
        {
            Debug.Log(trainingStage);

            textDelay = 3f;

        }

        if (trainingStage == 5)
        {
            Debug.Log(trainingStage);


        }

        if (trainingStage == 6)
        {
            Debug.Log(trainingStage);


        }
    }

    IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(textDelay);
        if (trainingStage == 0)
        {
            trainingCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (trainingStage == 2)
        {
            trainingCanvas.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (trainingStage == 3)
        {
            trainingCanvas.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (trainingStage == 4)
        {
            trainingCanvas.transform.GetChild(3).gameObject.SetActive(true);
        }
    }
}
