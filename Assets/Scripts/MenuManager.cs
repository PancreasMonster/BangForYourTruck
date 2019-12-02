using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    bool singlePlayerActive;
    public GameObject singlePlayer;
    public GameObject campaign;
    public GameObject practice;

    bool multiplayerActive;
    public GameObject multiPlayer;
    public GameObject splitscreen;
    public GameObject online;

    public GameObject Gallery;

    public GameObject Credits;

    // Start is called before the first frame update
    void Start()
    {
        //splitscreen = GameObject.Find("Split Screen");
        //online = GameObject.Find("Online");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSinglePlayerOptions()
    {
        if (!singlePlayerActive)
        {
            singlePlayerActive = true;
            campaign.SetActive(true);
            practice.SetActive(true);

            multiPlayer.SetActive(false);
            Gallery.SetActive(false);
            Credits.SetActive(false);
        }
        else
        {
            singlePlayerActive = false;
            campaign.SetActive(false);
            practice.SetActive(false);

            multiPlayer.SetActive(true);
            Gallery.SetActive(true);
            Credits.SetActive(true);
        }
    }

    public void ToggleMultiplayerOptions()
    {
        if (!multiplayerActive)
        {
            multiplayerActive = true;
            online.SetActive(true);
            splitscreen.SetActive(true);

            singlePlayer.SetActive(false);
            Gallery.SetActive(false);
            Credits.SetActive(false);
        }
        else
        {
            multiplayerActive = false;
            online.SetActive(false);
            splitscreen.SetActive(false);

            singlePlayer.SetActive(true);
            Gallery.SetActive(true);
            Credits.SetActive(true);
        }
    }

    public void LoadSplitScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
