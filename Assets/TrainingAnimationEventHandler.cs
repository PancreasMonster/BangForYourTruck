﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingAnimationEventHandler : MonoBehaviour
{
    public Camera playerCamera;
    public Camera droneCamera1;
    public Camera droneCamera2;

    public TrainingManager tm;

    public Animator droneFaceAnimator;

    public Transform lookForwardTransform;
    public Transform player;

    public void PlayKillHim() 
    {
        tm.PlayKILLHIMAudio();
    }

    public void LookAtCentreOfTheArena()
    {
        GetComponentInChildren<TrainingDrone>().lookAtTransform = lookForwardTransform;
    }

    public void LookAtPlayer()
    {
        GetComponentInChildren<TrainingDrone>().lookAtTransform = player.transform;
    }

    public void SwapToDroneCamera()
    {
        playerCamera.enabled = false;
        droneCamera2.enabled = false;
        droneCamera1.enabled = true;
    }

    public void SwapToChildedCamera()
    {
        playerCamera.enabled = false;
        droneCamera2.enabled = true;
        droneCamera1.enabled = false;
    }

    public void SwapToPlayerCamera()
    {
        droneCamera1.enabled = false;
        droneCamera2.enabled = false;
        playerCamera.enabled = true;
    }

    public void ShowWOWFace()
    {
        droneFaceAnimator.SetTrigger("Wow");
    }

    public void ShowOUCHFace()
    {
        droneFaceAnimator.SetTrigger("Ouch");
    }

    public void ShowSadFace()
    {
        droneFaceAnimator.SetTrigger("Sad");
    }

    public void GoBackToIdleFace()
    {
        droneFaceAnimator.SetTrigger("GoBack");
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("0_MainMenu", LoadSceneMode.Single);
    }
}
