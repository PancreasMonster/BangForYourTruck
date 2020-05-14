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
    public List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> exitButtons = new List<GameObject>();
    public GameObject controls;
    public GameObject continueButton, controlsBackButton, confirmButton, currentGameObject;
    public EventSystem e;


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

            foreach (GameObject g in exitButtons)
            {
                g.SetActive(false);
            }

            GameIsPaused = true;
            noJumpOrBoost = true;
            goToPauseMenu();
        }
        else
        {
            
        }
    }

    

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

            foreach(GameObject g in buttons)
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

            foreach (GameObject g in buttons)
            {
                g.SetActive(true);
            }

            foreach (GameObject g in exitButtons)
            {
                g.SetActive(false);
            }
        goToPauseMenu();
    } 

    public void goToPauseMenu()
    {
        e.SetSelectedGameObject(null);
        e.SetSelectedGameObject(continueButton);
        currentGameObject = e.currentSelectedGameObject;
    }

    public void goToControls()
    {
        e.SetSelectedGameObject(null);
        e.SetSelectedGameObject(controlsBackButton);
        currentGameObject = e.currentSelectedGameObject;
    }

    public void goToConfirm()
    {
        e.SetSelectedGameObject(null);
        e.SetSelectedGameObject(confirmButton);
        currentGameObject = e.currentSelectedGameObject;
    }
}
