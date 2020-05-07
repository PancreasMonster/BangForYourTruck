using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingAnimationEventHandler : MonoBehaviour
{
    public Camera playerCamera;
    public Camera droneCamera;


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
}
