using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingAnimationEventHandler : MonoBehaviour
{
    public Camera playerCamera;
    public Camera droneCamera;

    public Animator droneFaceAnimator;

    public void SwapToDroneCamera()
    {
        playerCamera.enabled = false;
        droneCamera.enabled = true;
    }

    public void SwapToPlayerCamera()
    {
        droneCamera.enabled = false;
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
}
