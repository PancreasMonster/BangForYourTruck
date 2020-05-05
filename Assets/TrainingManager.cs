using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TrainingManager : MonoBehaviour
{
    public GameObject player;
    public Camera camera;
    public GameObject drone;
    public int trainingStage = 0;
    GameObject trainingCanvas;
    BoxCollider[] triggers;
    public GameObject targetDrone;

    public float textDelay = 1f;
    AudioSource audio;
    Animator camAnim;
    Animator droneAnim;

    public bool pressedRT;
    public bool pressedLT;
    public bool targetDroneDestroyed;
    public bool killTokenDelivered;

    Orbit orbitScript;

    public bool trigger1 = false;
    public bool trigger2 = false;


    // Start is called before the first frame update
    void Start()
    {        
        player.GetComponent<RearWheelDrive>().trainingMode = true;
        orbitScript = player.GetComponent<Orbit>();
        triggers = GetComponentsInChildren<BoxCollider>();
        camAnim = camera.GetComponent<Animator>();
        droneAnim = drone.GetComponent<Animator>();
        trainingCanvas = GameObject.Find("Training Canvas");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trainingStage == 1)
        {
            //if player presses RT, pressedRT = true;
            //if player presses LT, pressedLT = true;

            if (pressedLT && pressedRT)
            {
                ProceedTraining();
            }

        }

        if (trainingStage == 2)
        {
            if (trigger1)

            ProceedTraining();

        }

        if (trainingStage == 3)
        {
            //player is locked onto a target

            if (player.GetComponent<LockOn>().target != null)
            {
                ProceedTraining();
            }

        }

        if (trainingStage == 4)
        {
            //player has to overheat their weapons

            if (player.GetComponent<PowerHolder>().powerAmount <= 50)
            {
                ProceedTraining();
            }

        }

        if (trainingStage == 5)
        {
            //player has to kill a training drone

            if (targetDroneDestroyed)
            {
                ProceedTraining();
            }

        }

        if (trainingStage == 6)
        {
            //player has deliver a killtoken to their collection gate

            if (trigger2)
            {
                ProceedTraining();
            }
        }

        if (trainingStage == 7)
        {
            //player is shown how to purchase throwables

            Invoke("ProceedTraining", 2f);
                
            
        }

        if (trainingStage == 8)
        {
            //Fade to black

            Invoke("ProceedTraining", 2f);

        }




    }

    public void ProceedTraining()
    {
        trainingStage++;
        Training();
        ClearText();
        audio.Play();
    }

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
            Invoke("DisplayText", 4f);
        }

        if (trainingStage == 3)
        {
            //drone should wait at waypoint 6 and look at the player at all times. 
            //Proceed when player locks onto a target
            Debug.Log(trainingStage);
            //textDelay = 3f;
            Debug.Log("DroneShouldMove");
            drone.GetComponent<TrainingDrone>().AdvanceToNextWaypoint();
            Invoke("DisplayText", textDelay);
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
