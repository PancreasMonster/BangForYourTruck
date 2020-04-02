using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerPause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public List<GameObject> UIElements = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();


    public EventSystem e;
    public GameObject g;

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
            e.SetSelectedGameObject(g);
            Time.timeScale = 0;
            GameIsPaused = true;
        }
        else
        {
            foreach (GameObject g in UIElements)
            {
                g.SetActive(false);
            }

            Time.timeScale = 1;
            GameIsPaused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Back1"))
        {
            
        }
    }

    public void Exit () 
    {
        SceneManager.LoadScene("0_MainMenu");
    }

    public void Unpause()
    {
        foreach (GameObject g in UIElements)
        {
            g.SetActive(false);
        }

        Time.timeScale = 1;
        GameIsPaused = false;
    }

}
