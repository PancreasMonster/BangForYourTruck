using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerConnectionManager : MonoBehaviour
{
    public bool ps4ControllerInEditor, showConnectedControllers;

    public List<PlayerInput> PI = new List<PlayerInput>();

    public List<Text> textItems = new List<Text>();

    public List<PlayerConnectItem> playerConnectItems = new List<PlayerConnectItem>();


    private void OnPlayerLeft(Text t)
    {
        Debug.Log("Left");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int conTrollerCount = 0;

        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {

            if (ps4ControllerInEditor)
            {
                if (names[x].Length == 19)
                {

                    //set a controller bool to true
                    conTrollerCount++;

                }
            }

            if (names[x].Length == 33)
            {

                //set a controller bool to true
                conTrollerCount++;

            }
        }


        if (showConnectedControllers)
            Debug.Log(conTrollerCount);

        if (conTrollerCount == 0)
            conTrollerCount = 1;


        for (int i = 0; i < PI.Count; i++)
        {
            if ((i + 1) <= conTrollerCount)
            {
                PI[i].enabled = true;
                textItems[i].text = playerConnectItems[i].connectState;
            }
            else
            {
                PI[i].enabled = false;
                textItems[i].text = "";
            }
        }
    }
}
