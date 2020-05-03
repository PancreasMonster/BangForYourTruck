using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TrainingManager : MonoBehaviour
{
    public Camera camera;
    public GameObject drone;
    public int trainingStage;
    GameObject trainingCanvas;
    BoxCollider[] triggers;

    public float textDelay;
    AudioSource audio;
    Animator camAnim;
    Animator droneAnim;

    // Start is called before the first frame update
    void Start()
    {
        triggers = GetComponentsInChildren<BoxCollider>();
        camAnim = camera.GetComponent<Animator>();
        droneAnim = drone.GetComponent<Animator>();
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
            //show drone coming to the player and talking to them
            droneAnim.SetTrigger("");
            camAnim.SetTrigger("");
            Debug.Log(trainingStage);
            textDelay = 2f;
            Invoke("DisplayText", 1f);
            Invoke("ProceedTraining", 2f);
        }

        if (trainingStage == 1)
        {
            Debug.Log(trainingStage);
            droneAnim.SetTrigger("");
            triggers[0].isTrigger = true;
            trainingCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (trainingStage == 2)
        {
            Debug.Log(trainingStage);
            droneAnim.SetTrigger("");
            textDelay = 3f;

        }

        if (trainingStage == 3)
        {
            Debug.Log(trainingStage);
            droneAnim.SetTrigger("");
            textDelay = 2f;

        }

        if (trainingStage == 4)
        {
            //look at hard to reach drone 
            Debug.Log(trainingStage);
            droneAnim.SetTrigger("");
            camAnim.SetTrigger("");
            textDelay = 3f;

        }

        if (trainingStage == 5)
        {
            //watch training drone die, look at killtoken, then look at collection gate 
            camAnim.SetTrigger("");
            droneAnim.SetTrigger("");
            Debug.Log(trainingStage);


        }

        if (trainingStage == 6)
        {
            //Closeup of drones face fade to black
            camAnim.SetTrigger("");
            droneAnim.SetTrigger("");
            Debug.Log(trainingStage);
            triggers[1].isTrigger = true;


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
