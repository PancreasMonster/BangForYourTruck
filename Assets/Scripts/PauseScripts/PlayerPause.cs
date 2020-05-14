using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerPause : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool noJumpOrBoost = false;
    public bool noPlayerInput = false;
    public bool tryingToLeave = false;
    public bool lookingAtControls = false;
    public List<GameObject> UIElements = new List<GameObject>();
    public GameObject continueButton, controlsBackButton, confirmButton, currentGameObject;
    public int i;
    public Vector2 leftStick;
    public bool canChange = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnStart(InputValue value)
    {
        if (GameIsPaused == false)
        {
            foreach (GameObject g in UIElements)
            {
                g.SetActive(true);
            }


            GameIsPaused = true;
            noJumpOrBoost = true;
            goToPauseMenu();
        }
        else
        {
            
        }
    }

    /*

    private void OnFaceButtonEast(InputValue value)
    {
        if(GameIsPaused && tryingToLeave)
        {
            Cancel();
        }
        else if (GameIsPaused && lookingAtControls)
        {
            StopControls();
        }
        else if (GameIsPaused && !lookingAtControls && !tryingToLeave)
        {
            Unpause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit () 
    {
        if(!tryingToLeave)
        {
            tryingToLeave = true;

            foreach(GameObject g in buttonGameObjects)
            {
                g.SetActive(false);
            }

            foreach(GameObject g in exitButtons)
            {
                g.SetActive(true);
            }
            goToConfirm();
        }
        else
        {
            SceneManager.LoadScene("0_MainMenu", LoadSceneMode.Single);
        }
    }

    public void Unpause()
    {
        foreach (GameObject g in UIElements)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in exitButtons)
        {
            g.SetActive(true);
        }

        GameIsPaused = false;
        noJumpOrBoost = false;
        lookingAtControls = false;
        tryingToLeave = false;
        controls.SetActive(false);
    }

    public void Controls ()
    {
        lookingAtControls = true;
        controls.SetActive(true);
        foreach (GameObject g in UIElements)
        {
            g.SetActive(false);
        }
        goToControls();
    }

    public void StopControls ()
    {
        lookingAtControls = false;
        controls.SetActive(false);
        foreach (GameObject g in UIElements)
        {
            g.SetActive(true);
        }
        goToPauseMenu();
    }

    public void Cancel()
    {

            tryingToLeave = false;

            foreach (GameObject g in buttonGameObjects)
            {
                g.SetActive(true);
            }

            foreach (GameObject g in exitButtons)
            {
                g.SetActive(false);
            }
        goToPauseMenu();
    } */

    public void goToPauseMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(continueButton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void goToControls()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void goToConfirm()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(confirmButton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }
}
