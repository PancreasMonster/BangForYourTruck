using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreator : MonoBehaviour

    
{

    public bool singlePlayer;
    public int levelToActivate;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("0_MainMenu", LoadSceneMode.Single);
        }
    }

    public void ToggleSinglePlayer()
    {
        singlePlayer = !singlePlayer;
    }

    public void SetLevelIndex (int i)
    {
        levelToActivate = i;
        LoadScene();
    }

    public void LoadScene()
    {
        if(singlePlayer)
        {
            SceneManager.LoadScene("SinglePlayerScene", LoadSceneMode.Single);
            
        } else
        {
            SceneManager.LoadScene("Alpha Playtest", LoadSceneMode.Single);
            
        }
    }
   
}
