using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;



public class ControlManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public List<PlayerInput> PI = new List<PlayerInput>();

    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject p in GameObject.FindObjectOfType<KillManager>().players)
        {
            PI.Add(p.GetComponent<PlayerInput>());
        }

        StartCoroutine(waitTime());


        ControllersAdded();
        //PlayerInputs();
    }

   
    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(3);


        foreach (PlayerInput pi in PlayerInput.all)
        {

       
            pi.user.UnpairDevices();

        }




        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            InputUser.PerformPairingWithDevice(Gamepad.all[i], PI[i].user);
            foreach (InputDevice id in PI[i].user.pairedDevices)
                Debug.Log(PI[i].name + " has: " + id);
        }

        Debug.Log("Please");

        
    }

    public void PlayerInputs()
    {


        players = GameObject.FindObjectOfType<KillManager>().players;



        foreach (InputDevice d in Gamepad.all)
        {
            Debug.Log(d.displayName.ToString());
        }

        int i = 0;
        foreach (GameObject p in players)
        {
            PlayerInput pi = null;
            if (p.GetComponent<PlayerInput>())
                pi = p.GetComponent<PlayerInput>();
            else
                pi = p.AddComponent<PlayerInput>();
            PI.Add(pi);

        }
    }

    public void ControllersAdded()
    {
        //Input.GetJoystickNames().
        int conTrollerCount = 0;

        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 33)
            {

                //set a controller bool to true
                conTrollerCount++;

            }
        }

        for (int i = 0; i < PI.Count; i++)
        {

            if ((i + 1) <= conTrollerCount)
            {
                PI[i].enabled = true;
            }
            else
            {
                PI[i].enabled = false;
            }
        }
    }
}
