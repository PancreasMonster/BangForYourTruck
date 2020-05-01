using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreator : MonoBehaviour

    
{
    public bool mainMenu;
    public bool training, devRoom, twoPlayer, fourPlayer;
    public int levelToActivate;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !mainMenu)
        {
            SceneManager.LoadScene("0_MainMenu", LoadSceneMode.Single);
            Destroy(this.gameObject);
        }
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


    public void SetLevelIndex (int i)
    {
        levelToActivate = i;
        //LoadScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        mainMenu = false;
        if(training)
        {
            SceneManager.LoadScene("SinglePlayerScene", LoadSceneMode.Single);
            
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
