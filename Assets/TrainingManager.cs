using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TrainingManager : MonoBehaviour
{
    public GameObject player;
    public GameObject drone;
    public int trainingStage = 0;
    GameObject trainingCanvas;
    public BoxCollider[] triggers;
    public GameObject targetDrone;
    public AudioClip[] trainingAudioClips;

    public float textDelay = 1f;
    AudioSource audio;
    Animator droneAnim;

    public bool pressedRT;
    public bool pressedLT;

    public bool trigger1 = false;

    public bool pressedA;
    public bool pressedB;

    public bool driftCompleted;

    public bool targetDroneDestroyed;

    public bool killTokenDelivered;

    Orbit orbitScript;

    public KillTagSpawner tagSpawner;
    public bool trigger2 = false;

    public bool canProceed = false;

    MobilityCharges mc;

    TrainingDrone td;

    [FMODUnity.EventRef]
    public string welcome;

    [FMODUnity.EventRef]
    public string voiceline1;

    [FMODUnity.EventRef]
    public string voiceline2;

    [FMODUnity.EventRef]
    public string voiceline3;

    [FMODUnity.EventRef]
    public string voiceline4;

    [FMODUnity.EventRef]
    public string voiceline5;

    [FMODUnity.EventRef]
    public string voiceline6;

    [FMODUnity.EventRef]
    public string voiceline7;

    [FMODUnity.EventRef]
    public string voiceline8;

    [FMODUnity.EventRef]
    public string droneInArena;

    [FMODUnity.EventRef]
    public string killHim;

    bool playSound = true;

    public KillTagSpawner kts;

    // Start is called before the first frame update
    void Start()
    {        
        player.GetComponent<RearWheelDrive>().trainingMode = true;
        //player.GetComponent<TrainingCheck>().enabled = true;
        droneAnim = drone.GetComponentInParent<Animator>();
        trainingCanvas = GameObject.Find("Training Canvas");
        audio = GetComponent<AudioSource>();
        trainingCanvas.SetActive(true);
        drone.SetActive(true);
        triggers = GetComponentsInChildren<BoxCollider>();
        mc = player.GetComponent<MobilityCharges>();
        td = drone.GetComponent<TrainingDrone>();
        FMODUnity.RuntimeManager.PlayOneShot(welcome);
    }

    // Update is called once per frame
    void Update()
    {
        if (trainingStage == 1)
        {
            if(playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline1);
                playSound = false;
                StartCoroutine(CanProceed(4.5f));
            }
            //Drone explains the driving controls
            if(audio)
            audio.Play();
            Invoke("DisplayText", 3f);

            if (pressedLT && pressedRT)
            {
                trainingStage = 2;
                ClearText();
                triggers[0].isTrigger = true;
                triggers[1].isTrigger = true;
                playSound = true;
            }

        }

        if (trainingStage == 2)
        {
            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline2);
                playSound = false;
            }
            //Drone explains to select a weapon
            if (audio)
                audio.clip = trainingAudioClips[1];
            Invoke("DisplayText", 2f);
            if (audio)
                audio.Play();

           

        }

        if (trainingStage == 3)
        {
            if (playSound)
            {
                StartCoroutine(PlayVoiceLine3());
                playSound = false;
            }
            //Drone explains A, B, X, Y controls, and tells you to try them all
            if (audio)
                audio.clip = trainingAudioClips[2];
            if (audio)
                audio.Play();
            Invoke("DisplayText", 4f);
            if (!mc.charge1 && !mc.charge2 && !mc.charge3)
            {
                ClearText();
                playSound = true;
                trainingStage = 4;
            }
        }

        if (trainingStage == 4)
        {

            if (playSound) 
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline4);
                playSound = false;
            }
            //Drone explains drifting and the benefits
            if (audio)
                audio.clip = trainingAudioClips[3];
            if (audio)
                audio.Play();
            Invoke("DisplayText", 5f);
            if (driftCompleted)
            {
                ClearText();
                trainingStage = 5;
                playSound = true;
            }
        }

        if (trainingStage == 5)
        {
            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline5);
                playSound = false;
            }
            //Drone explains how to fire your weapon
            if (audio)
                audio.clip = trainingAudioClips[4];
            if (audio)
                audio.Play();
            Invoke("DisplayText", 2f);

            if (player.GetComponent<PowerHolder>().powerAmount <= 50)
            {
                ClearText();
                trainingStage = 6;
                playSound = true;
                targetDrone.GetComponentInChildren<Health>().enabled = true;
                droneAnim.SetBool("ProceedTraining2", true);

            }

        }

        if (trainingStage == 6)
        {
            if (playSound) 
            {
                FMODUnity.RuntimeManager.PlayOneShot(droneInArena);
                playSound = false;
            }

            //Drone tells you to kill the target drone
            if (audio)
                audio.clip = trainingAudioClips[5];
            if (audio)
                audio.Play();
            Invoke("DisplayText", 5f);

            if (targetDroneDestroyed)
            {
                player.GetComponent<LockOn>().targets.Clear();
                ClearText();
                trainingStage = 7;
                playSound = true;
                droneAnim.SetBool("ProceedTraining3", true);
                triggers[2].isTrigger = true;
            }

        }

        if (trainingStage == 7)
        {
            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline6);
                playSound = false;
            }

            //Drone tells the player to deliver a killtoken to their collection gate
            if (audio)
                audio.clip = trainingAudioClips[6];
            Invoke("DisplayText", 5f);
            if (audio)
                audio.Play();

        }

        if (trainingStage == 8)
        {

            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline7);
                playSound = false;
            }
            Invoke("DisplayText", 1f);

            //player is shown how to purchase throwables
            if (audio)
                audio.clip = trainingAudioClips[7];
            trainingCanvas.transform.GetChild(7).gameObject.SetActive(true);
            if (audio)
                audio.Play();

            Invoke("ProceedToTraining9", 6f);// this Invoke needs to last as long as the drones purchase gate audioclip
            droneAnim.SetBool("ProceedTraining5", true);


        }

        if (trainingStage == 9)
        {
            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline8);
                playSound = false;
            }
            DisplayText();

            //Fade to black
            if (audio)
                audio.clip = trainingAudioClips[8];
            trainingCanvas.transform.GetChild(8).gameObject.SetActive(true);
            if (audio)
                audio.Play();

        }
    }

    public void ProceedToTraining1()
    {
        playSound = true;
        trainingStage = 1;
    }

    public void ProceedToTraining9()
    {
        trainingStage = 9;
        ClearText();
        playSound = true;
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
        else if (trainingStage == 9)
        {
            trainingCanvas.transform.GetChild(8).gameObject.SetActive(true);
        }
    }

    public void DroneKilled()
    {
        StartCoroutine(DroneKilledCoroutine());
    }

    IEnumerator DroneKilledCoroutine()
    {
        yield return new WaitForSeconds(2);
        targetDroneDestroyed = true;
        kts.SpawnKillTag();
    }

    public void Drift()
    {
        Debug.Log("Drifted");
        if (trainingStage == 4)
            driftCompleted = true;
    }

    public void MoveToArena()
    {
        StartCoroutine(MoveToArenaCoroutine());
    }

    IEnumerator MoveToArenaCoroutine()
    {
        playSound = true;
        td.AdvanceToNextWaypoint();
        td.droneSpeed = 3000;
        ClearText();
        yield return new WaitForSeconds(1.5f);
        td.droneSpeed = 800;
        yield return new WaitForSeconds(3.5f);
        td.BarrelRoll(10000);
        droneAnim.SetBool("ProceedTraining1", true);
    }

    public void TagDeposit()
    {
        StartCoroutine(TagDepositCoroutine());
    }

    IEnumerator TagDepositCoroutine()
    {
        yield return new WaitForSeconds(0);
        ClearText();
        trainingStage = 8;
        droneAnim.SetBool("ProceedTraining4", true);
    }

    IEnumerator PlayVoiceLine3()
    {
        yield return new WaitForSeconds(4);
        FMODUnity.RuntimeManager.PlayOneShot(voiceline3);
        playSound = false;
    }

    public void PlayKILLHIMAudio() 
    {
        playSound = true;
        FMODUnity.RuntimeManager.PlayOneShot(killHim);
        playSound = false;
    }

    IEnumerator CanProceed(float time)
    {
        yield return new WaitForSeconds(time);
        canProceed = true;
    }
}
