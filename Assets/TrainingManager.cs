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
    public GameObject[] triggers;
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

    [FMODUnity.EventRef]
    public string waitForMe;

    [FMODUnity.EventRef]
    public string wooHoo;

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
        mc = player.GetComponent<MobilityCharges>();
        td = drone.GetComponent<TrainingDrone>();
        StartCoroutine(Welcome());
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
                StartCoroutine(DisplayText());
            }
            //Drone explains the driving controls
            //if(audio)
            //audio.Play();
            //Invoke("DisplayText", 4f);

            if (pressedLT && pressedRT)
            {
                trainingStage = 2;
                ClearText();
                triggers[0].GetComponent<BoxCollider>().isTrigger = true;
                triggers[0].GetComponent<MeshRenderer>().enabled = false;
                triggers[1].GetComponent<BoxCollider>().isTrigger = true;
                playSound = true;
            }

        }

        if (trainingStage == 2)
        {
            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline2);
                playSound = false;
                StartCoroutine(DisplayText());

            }
            //Drone explains to select a weapon
            //if (audio)
            //audio.clip = trainingAudioClips[1];
            //if (audio)
               // audio.Play();

           

        }

        if (trainingStage == 3)
        {
            if (playSound)
            {
                canProceed = false;
                ClearText();
                StartCoroutine(PlayVoiceLine3());
                playSound = false;
                StartCoroutine(DisplayText());
                StartCoroutine(CanProceed(16));
            }
            //Drone explains A, B, X, Y controls, and tells you to try them all
            
            if (!mc.charge1 && !mc.charge2 && !mc.charge3 && canProceed)
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
                canProceed = false;
                FMODUnity.RuntimeManager.PlayOneShot(voiceline4);
                playSound = false;
                StartCoroutine(DisplayText());
                StartCoroutine(CanProceed(7.2f));

            }
            //Drone explains drifting and the benefits
            
            if (driftCompleted && canProceed)
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
                StartCoroutine(DisplayText());
                canProceed = false;
                StartCoroutine(CanProceed(4.5f));
            }
            //Drone explains how to fire your weapon
            

            if (player.GetComponent<PowerHolder>().powerAmount <= 50 && canProceed)
            {
                ClearText();
                trainingStage = 6;
                playSound = true;
                
                droneAnim.SetBool("ProceedTraining2", true);

            }

        }

        if (trainingStage == 6)
        {
            if (playSound) 
            {
                FMODUnity.RuntimeManager.PlayOneShot(droneInArena);
                playSound = false;
                StartCoroutine(DisplayText());
                canProceed = false;
                StartCoroutine(CanProceed(5f));
                StartCoroutine(HealthEnable(5f));
            }

            //Drone tells you to kill the target drone
            

            if (targetDroneDestroyed && canProceed)
            {
                player.GetComponent<LockOn>().targets.Clear();
                ClearText();
                trainingStage = 7;
                playSound = true;
                droneAnim.SetBool("ProceedTraining3", true);
                //triggers[2].GetComponent<BoxCollider>().isTrigger = true;
            }

        }

        if (trainingStage == 7)
        {
            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline6);
                playSound = false;
                StartCoroutine(DisplayText());

            }

            //Drone tells the player to deliver a killtoken to their collection gate
            

        }

        if (trainingStage == 8)
        {

            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline7);
                playSound = false;
                StartCoroutine(DisplayText());
                StartCoroutine(GoToTrainingStage9());

            }

            //player is shown how to purchase throwables



        }

        if (trainingStage == 9)
        {
            if (playSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(voiceline8);
                playSound = false;
                StartCoroutine(DisplayText());

            }

            //Fade to black
           
            droneAnim.SetBool("ProceedTraining5", true);

        }
    }

    public void ProceedToTraining1()
    {
        playSound = true;
        trainingStage = 1;
    }


    void ClearText()
    {
        for (int i = 0; i < trainingCanvas.transform.childCount; i++)
        {
            trainingCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    IEnumerator DisplayText()
    {

        if (trainingStage == 1)
        {
            textDelay = 5f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (trainingStage == 2)
        {
            textDelay = .5f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (trainingStage == 3)
        {
            textDelay = 16f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (trainingStage == 4)
        {
            textDelay = 7.2f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (trainingStage == 5)
        {
            textDelay = 4.5f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (trainingStage == 6)
        {
            textDelay = 5f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (trainingStage == 7)
        {
            textDelay = 10f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (trainingStage == 8)
        {
            textDelay = 7f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(7).gameObject.SetActive(true);
        }
        else if (trainingStage == 9)
        {
            textDelay = .5f;
            yield return new WaitForSeconds(textDelay);
            ClearText();
            trainingCanvas.transform.GetChild(8).gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            ClearText();

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

    IEnumerator GoToTrainingStage9()
    {
        yield return new WaitForSeconds(10);
        trainingStage = 9;
        playSound = true;
    }

    public void Drift()
    {
        Debug.Log("Drifted");
        if (trainingStage == 4)
            driftCompleted = true;
    }

    public void MoveToArena()
    {
        ClearText();

        StartCoroutine(MoveToArenaCoroutine());
    }

    IEnumerator MoveToArenaCoroutine()
    {
        playSound = true;
        td.AdvanceToNextWaypoint();
        td.droneSpeed = 3000;
        ClearText();
        FMODUnity.RuntimeManager.PlayOneShot(waitForMe);
        yield return new WaitForSeconds(1.5f);
        td.droneSpeed = 800;
        yield return new WaitForSeconds(1.5f);
        FMODUnity.RuntimeManager.PlayOneShot(wooHoo);
        yield return new WaitForSeconds(2f);
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
        playSound = true;
        droneAnim.SetBool("ProceedTraining4", true);
    }

    IEnumerator PlayVoiceLine3()
    {
        ClearText();

        yield return new WaitForSeconds(8);
        FMODUnity.RuntimeManager.PlayOneShot(voiceline3);
        playSound = false;
        Invoke("DisplayText", 12f);

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

    IEnumerator Welcome() 
    {
        yield return new WaitForSeconds(2);
        FMODUnity.RuntimeManager.PlayOneShot(welcome);

    }

    IEnumerator HealthEnable(float time)
    {
        yield return new WaitForSeconds(time);
        targetDrone.GetComponentInChildren<Health>().enabled = true;
    }
}
