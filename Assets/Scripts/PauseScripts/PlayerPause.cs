using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public List<GameObject> UIElements = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Back1"))
        {
            if (GameIsPaused == false)
            {
                foreach(GameObject g in UIElements)
                {
                    g.SetActive(true);
                }
                GameIsPaused = true;
            }
            else
            {
                foreach (GameObject g in UIElements)
                {
                    g.SetActive(false);
                }
                GameIsPaused = false;
            }
        }
    }

    public void Exit () 
    {
        SceneManager.LoadScene("0_MainMenu");
    }
}
