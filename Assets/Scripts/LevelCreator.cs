using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCreator : MonoBehaviour

    
{
    public bool mainMenu;
    public bool training, devRoom, twoPlayer, fourPlayer;
    public int levelToActivate;
    public float volumeSetting;
    public GameObject volumeSlider;
    public bool hints;
    public bool AVCrowd;
    public bool credits;


    // Start is called before the first frame update
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrainingTrue()
    {
    
        training = true;
        
    }

    public void SetTrainingFalse()
    {

        training = false;

    }

    public void SetDevRoomTrue()
    {

        devRoom = true;

    }

    public void SetDevRoomFalse()
    {

        devRoom = false;

    }

    public void Set1v1False()
    {

        twoPlayer = false;

    }

    public void Set1v1True()
    {

        twoPlayer = true;

    }

    public void Set2v2False()
    {

        fourPlayer = false;

    }

    public void Set2v2True()
    {

        fourPlayer = true;

    }

    public void GetVolumeSliderValue()
    {
        volumeSetting = volumeSlider.GetComponent<Slider>().value;
    }


    public void SetLevelIndex (int i)
    {
        PlayerPrefs.SetInt("LevelToLoad", i);
        //LoadScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleHints() 
    {

        
        if (PlayerPrefs.GetInt("hintsOn") == 1)
        {
            PlayerPrefs.SetInt("hintsOn", 0);
        } else if (PlayerPrefs.GetInt("hintsOn") == 0)
        {
            PlayerPrefs.SetInt("hintsOn", 1);
        }

    }

    public void ToggleAVCrowd()
    {
        if (PlayerPrefs.GetInt("AVOn") == 1)
        {
            PlayerPrefs.SetInt("AVOn", 0);
        }
        else if (PlayerPrefs.GetInt("AVOn") == 0)
        {
            PlayerPrefs.SetInt("AVOn", 1);
        }
    }

    public void ToggleCredits()
    {
        if (credits)
        {
            credits = false;
        }
        else
        {
            credits = true;
        }
    }

    public void LoadScene()
    {
        mainMenu = false;
        if(training)
        {
            SceneManager.LoadScene("Gold SinglePlayer", LoadSceneMode.Single);
            
        }

        if (devRoom)
        {
            SceneManager.LoadScene("Gold Dev Room", LoadSceneMode.Single);

        }
        if (twoPlayer){
            SceneManager.LoadScene("Gold 1v1", LoadSceneMode.Single);
            
        }
        if (fourPlayer)
        {
            SceneManager.LoadScene("Alpha Playtest", LoadSceneMode.Single);

        }
    }
   
}
