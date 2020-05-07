using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TrainingManager : MonoBehaviour
{
    public GameObject player;
    public Camera camera1;
    public Camera camera2;
    public GameObject drone;
    public int trainingStage = 0;
    GameObject trainingCanvas;
    public BoxCollider[] triggers;
    public GameObject targetDrone;
    public AudioClip[] clips;

    public float textDelay = 1f;
    AudioSource audio;
    Animator droneAnim;

    public bool pressedRT;
    public bool pressedLT;
    public bool pressedX;
    public bool pressedA;
    public bool pressedB;
    public bool pressedY;
    public bool targetDroneDestroyed;
    public bool killTokenDelivered;

    Orbit orbitScript;

    public bool trigger1 = false;
    public bool trigger2 = false;


    // Start is called before the first frame update
    void Start()
    {        
        player.GetComponent<RearWheelDrive>().trainingMode = true;
        //player.GetComponent<TrainingCheck>().enabled = true;
        droneAnim = drone.GetComponent<Animator>();
        trainingCanvas = GameObject.Find("Training Canvas");
        audio = GetComponent<AudioSource>();
        trainingCanvas.SetActive(true);
        drone.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (trainingStage == 1)
        {
            //Drone explains the driving controls
            audio.Play();

            if (pressedLT && pressedRT)
            {
                trainingStage = 2;
                ClearText();
                triggers[0].isTrigger = true;
                triggers[1].isTrigger = true;
            }

        }

        if (trainingStage == 2)
        {
            //Drone explains to select a weapon
            audio.clip = clips[1];
            audio.Play();

            if (trigger1)

            trainingStage = 3;
            ClearText();
            droneAnim.SetBool("ProceedTraining1",true);

        }

        if (trainingStage == 3)
        {
            //Drone explains A, B, X, Y controls, and tells you to try them all
            audio.clip = clips[2];
            audio.Play();

            if (pressedA && pressedB && pressedX && pressedY)
            {
                ClearText();
                trainingStage = 4;
            }

        }

        if (trainingStage == 4)
        {
            //Drone explains how to fire your weapon
            audio.clip = clips[3];
            audio.Play();

            if (player.GetComponent<PowerHolder>().powerAmount <= 50)
            {
                ClearText();
                trainingStage = 5;
                targetDrone.GetComponentInChildren<Health>().enabled = true;
                droneAnim.SetBool("ProceedTraining2", true);

            }

        }

        if (trainingStage == 5)
        {
            //Drone tells you to kill the target drone
            audio.clip = clips[4];
            audio.Play();

            if (targetDroneDestroyed)
            {
                ClearText();
                trainingStage = 6;
                droneAnim.SetBool("ProceedTraining3", true);
                triggers[2].isTrigger = true;
            }

        }

        if (trainingStage == 6)
        {
            //Drone tells the player to deliver a killtoken to their collection gate
            audio.clip = clips[5];
            audio.Play();

            if (trigger2)
            {
                ClearText();
                trainingStage = 7;
                droneAnim.SetBool("ProceedTraining4", true);
            }
        }

        if (trainingStage == 7)
        {
            //player is shown how to purchase throwables
            audio.clip = clips[6];
            audio.Play();

            Invoke("ProceedToTraining8", 2f);// this Invoke needs to last as long as the drones purchase gate audioclip
            droneAnim.SetBool("ProceedTraining5", true);


        }

        if (trainingStage == 8)
        {
            //Fade to black
            audio.clip = clips[7];
            audio.Play();

        }
    }

    public void ProceedToTraining1()
    {
        trainingStage = 1;
        ClearText();
    }

    public void ProceedToTraining8()
    {
        trainingStage = 8;
        ClearText();
    }
/*
    void Training()
    {
        if (trainingStage == 1)
        {
            //show drone coming to the player and talking to them, drone should wait at waypoint 4 and look at the player at all times. 
            //Proceed when player has pressed both RT and LT
            camAnim.SetTrigger("");
            Debug.Log(trainingStage);
            //textDelay = 2f;
            Invoke("DisplayText", 1f);

        }

        if (trainingStage == 2)
        {
            //drone should wait at waypoint 4 and look at the player at all times. 
            //Proceed when player enters trigger(0)
            Debug.Log(trainingStage);
            triggers[0].isTrigger = true;
            Invoke("DisplayText", 1f);
        }

        if (trainingStage == 3)
        {
            //drone should wait at waypoint 6 and look at the player at all times. 
            //Proceed when player locks onto a target
            Debug.Log(trainingStage);
            //textDelay = 3f;
            Debug.Log("DroneShouldMove");
            drone.GetComponent<TrainingDrone>().AdvanceToNextWaypoint();
            Invoke("DisplayText", 8f);
        }

        if (trainingStage == 4)
        {
            //proceed to next training when player overheats their weapon, and enters a trigger on the camera drone "Come back to me when you're ready"
            Debug.Log(trainingStage);
            //textDelay = 2f;
            Invoke("DisplayText", textDelay);

        }

        if (trainingStage == 5)
        {
            //stop and look at hard to reach drone, player must kill it (presumably with a missile)
            Debug.Log(trainingStage);
            //orbitScript.enabled = false;
            camAnim.SetTrigger("");
            //textDelay = 3f;
            Invoke("DisplayText", textDelay);

        }

        if (trainingStage == 6)
        {
            //watch training drone die, look at killtoken, then look at collection gate. Proceed when player delivers it to the collection gate
            camAnim.SetTrigger("");
            Debug.Log(trainingStage);
            triggers[1].isTrigger = true;
            Invoke("DisplayText", textDelay);

        }

        if (trainingStage == 7)
        {
            //Show purchase gate and explain disc selection
            //orbitScript.enabled = false;
            camAnim.SetTrigger("");
            Debug.Log(trainingStage);
            Invoke("DisplayText", textDelay);
            Invoke("ProceedTraining", 5f);

        }

        if (trainingStage == 8)
        {
            //CONGRATULATIONS, fade to black
            //orbitScript.enabled = false;
            camAnim.SetTrigger("");
            Debug.Log(trainingStage);
            Invoke("DisplayText", textDelay);

        }
    }
    */
    void ClearText()
    {
        for (int i = 0; i < trainingCanvas.transform.childCount; i++)
        {
            trainingCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    void DisplayText()
    {
        if (trainingStage == 1)
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
        else if (trainingStage == 5)
        {
            trainingCanvas.transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (trainingStage == 6)
        {
            trainingCanvas.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (trainingStage == 7)
        {
            trainingCanvas.transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (trainingStage == 8)
        {
            trainingCanvas.transform.GetChild(7).gameObject.SetActive(true);
        }
    }

    public void DroneKilled()
    {
            targetDroneDestroyed = true;
    }
}
