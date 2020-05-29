using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreBoard : MonoBehaviour
{
    public TagCollectionManager tcm;
    public List<GameObject> scoreBoardItems = new List<GameObject>();
    public bool changeToScoreBoard = false;
    PlayerPause pp;

    private void OnSelect(InputValue value)
    {
        Debug.Log("Yes");
        if(!tcm.scoreBoardShown && !changeToScoreBoard && !pp.GameIsPaused)
        {
            changeToScoreBoard = true;
            foreach(GameObject g in scoreBoardItems)
            {
                g.SetActive(true);
            }
        }
        else if (!tcm.scoreBoardShown && changeToScoreBoard)
        {
            changeToScoreBoard = false;
            foreach (GameObject g in scoreBoardItems)
            {
                g.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pp = GetComponent<PlayerPause>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseScoreboard()
    {
        changeToScoreBoard = false;
        foreach (GameObject g in scoreBoardItems)
        {
            g.SetActive(false);
        }
    }
}
