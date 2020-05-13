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
    public List<Button> buttons = new List<Button>();
    public List<GameObject> buttonGameObjects = new List<GameObject>();
    public List<GameObject> exitButtons = new List<GameObject>();
    public GameObject controls;
    public int i;
    public Vector2 leftStick;
    public bool canChange = true;
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
            i = 0;


            for (int b = 0; b < buttons.Count; b++)
            {
                if (b == 0)
                {
                    buttons[b].GetComponent<Image>().color = buttons[b].colors.highlightedColor;
                }
                else
                {
                    buttons[b].GetComponent<Image>().color = buttons[b].colors.normalColor;
                }
            }
        }
        else
        {
            Unpause();
        }
    }

    private void OnLeftStick(InputValue value)
    {
        leftStick = value.Get<Vector2>();

        if (!tryingToLeave && !lookingAtControls)
        {
            if (leftStick.y > .8f && canChange && GameIsPaused)
            {
                canChange = false;
                if (i == 0)
                {
                    i = buttons.Count - 1;
                }
                else
                {
                    i--;
                }

            }
            else if (leftStick.y < .8f && leftStick.y > -.8f)
            {
                canChange = true;
            }
            else if (leftStick.y < -.8f && canChange && GameIsPaused)
            {
                canChange = false;
                if (i == buttons.Count - 1)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }

            }
            for (int b = 0; b < buttons.Count; b++)
            {
                if (b == i)
                {
                    buttons[b].GetComponent<Image>().color = buttons[b].colors.highlightedColor;
                }
                else
                {
                    buttons[b].GetComponent<Image>().color = buttons[b].colors.normalColor;
                }
            }

        }
    }

    private void OnFaceButtonSouth(InputValue value)
    {
        if(GameIsPaused)
        {
           if(i == 0)
            {
                Unpause();
            } else if (i == 1)
            {
                Controls();
            } else if (i == 2)
            {
                Exit();
            }
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
        } else if (GameIsPaused && !lookingAtControls && !&& tryingToLeave)
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
    }

    public void StopControls ()
    {
        lookingAtControls = false;
        controls.SetActive(false);
        foreach (GameObject g in UIElements)
        {
            g.SetActive(true);
        }
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
        
    }

    public void ChangeToMouse (int ctm)
    {
        for (int b = 0; b < buttons.Count; b++)
        {
            if (b == ctm)
            {
                buttons[b].GetComponent<Image>().color = buttons[b].colors.highlightedColor;
            }
            else
            {
                buttons[b].GetComponent<Image>().color = buttons[b].colors.normalColor;
            }
        }
        i = ctm;
    }
}
