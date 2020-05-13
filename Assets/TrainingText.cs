using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrainingText : MonoBehaviour
{
    public int trainingStage;
    bool isTraining;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        /*if (GameObject.Find("PersistentSceneLoader").GetComponent<LevelCreator>().singlePlayer == true)
        {
            isTraining = true;
        }
        else
        {
            isTraining = false;
        }*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("Pressed start");
            GoToNextTraining();

            //if (trainingStage == 1 || trainingStage ==2)
            //{

            //}           
        }
    }

    

    public void GoToNextTraining()
    {
        trainingStage++;
        Debug.Log("Going to next training");

        if (trainingStage == 1)
        {
            GoToJumpDriftBoostLockOnText();
        }

        if (trainingStage == 2)
        {
            GoToFightingText();
        }

        if (trainingStage == 3)
        {
            GoToCollectingTagsText();
        }

        if (trainingStage == 4)
        {
            GoToBuyingThrowablesText();
        }

        if (trainingStage == 5)
        {
            GoToUsingThrowablesText();
        }
    }

    public void GoToJumpDriftBoostLockOnText()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void GoToFightingText()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void GoToCollectingTagsText()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void GoToBuyingThrowablesText()
    {
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
    }

    public void GoToUsingThrowablesText()
    {
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(true);
    }
}
