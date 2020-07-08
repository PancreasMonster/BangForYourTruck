using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConnectItem : MonoBehaviour
{
    public bool player1;

    public string connectState;

    public string connectName;

    private void OnDeviceLost(InputValue value)
    {
        if (!player1)
            connectState = "Press X to Join";
    }

    private void OnFaceButtonWest(InputValue value)
    {
        if (!player1)
            connectState = connectName;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (player1)
            connectState = "Player 1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
