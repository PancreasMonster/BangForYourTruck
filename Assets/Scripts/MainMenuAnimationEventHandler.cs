using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuAnimationEventHandler : MonoBehaviour
{
    Animator anim;
    public GameObject playButton, optionsButton, firstOptionsButton, singlePlayerButton, multiPlayerButton, trainingMode, devRoom, oneVOnebutton, twoVTwobutton;
    public GameObject ggTraining, ggDevRoom, ggone, ggtwo;
    public GameObject currentGameObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(currentGameObject);
        }
    }

    /* public void GoToSinglePlayer()
     {
         anim.SetBool("SinglePlayerMenu", true);
     }

     public void GoToMultiPlayer()
     {
         anim.SetBool("MultiPlayerMenu", true);
     }

     public void LeaveSinglePlayer()
     {
         anim.SetBool("SinglePlayerMenu", false);
     }

     public void LeaveMultiPlayer()
     {
         anim.SetBool("MultiPlayerMenu", false);
     } */

    public void OnMouseUp()
    {
        
    }

    public void ToPlayModes()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(singlePlayerButton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void ToOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionsButton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void BackFromPlayMode()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void BackFromOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsButton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void ToSinglePlayerModes()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(trainingMode);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void ToTrainingModes()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ggTraining);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void ToDevRoom()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ggDevRoom);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void BackFromTrainingModes()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(trainingMode);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void BackFromDevRoom()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(devRoom);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void BackToSinglePlayerModes()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(trainingMode);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void ToMultiplayerModes()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(oneVOnebutton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void ToOneVOne()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ggone);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void BackFromOneVOne()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(oneVOnebutton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void ToTwoVTwo()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ggtwo);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void BackFromTwoVTwo()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(twoVTwobutton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void BackToMultiplayerModes()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(multiPlayerButton);
        currentGameObject = EventSystem.current.currentSelectedGameObject;
    }

}
