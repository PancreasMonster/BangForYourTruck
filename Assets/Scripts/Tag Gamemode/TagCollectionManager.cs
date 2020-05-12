using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TagCollectionManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string redWinSound;

    [FMODUnity.EventRef]
    public string blueWinSound;

    public Text winText;
    public Image redImage, blueImage, winPanelImage;
    public float blueTeamTokens, redTeamTokens, gameWinningAmount = 30;
    public bool gameWon, allowSceneChange;
    public Camera winCam;
    public RotateAroundLevel ral;
    public List<GameObject> canvasToDisable = new List<GameObject>();
    public float gameTimer = 600;
    public Text gameTimerText;
    public Text blueTeamTokenString, redTeamTokenString;
    VictoryDisplayStats vds;
    AwardScene awardScene;
    bool redTeamWon, blueTeamWon;
    public Transform targetPosition;
    public KillManager km;
    public bool countDownDisabled = false;
    public GameObject cameras;
    bool wentToAwards;
    public GameObject awardsHeader, rematchButton;
    public AudioSource[] aud;


    // Start is called before the first frame update
    void Start()
    {
        vds = GetComponent<VictoryDisplayStats>();
        awardScene = GetComponent<AwardScene>();
        if (countDownDisabled)
        {
            gameTimerText.text = "Infinite";
        }
    }

    void Update()
    {
        if (!gameWon)
        {
            redImage.fillAmount = redTeamTokens / gameWinningAmount;
            blueImage.fillAmount = blueTeamTokens / gameWinningAmount;
            blueTeamTokenString.text = blueTeamTokens.ToString();
            redTeamTokenString.text = redTeamTokens.ToString();
        }

        if (blueTeamTokens >= gameWinningAmount && !gameWon)
        {
            gameWon = true;
            winText.text = "Blue Team Wins!";
            StartCoroutine(Victory("Blue Team Wins!"));
            FMODUnity.RuntimeManager.PlayOneShot(blueWinSound);
            blueTeamWon = true;
        }
        else if (redTeamTokens >= gameWinningAmount && !gameWon)
        {
            gameWon = true;
            winText.text = "Red Team Wins!";
            StartCoroutine(Victory("Red Team Wins!"));
            FMODUnity.RuntimeManager.PlayOneShot(redWinSound);
            redTeamWon = true;
        }

        

        if(gameTimer >= 0 && !gameWon && !countDownDisabled)
        {
            gameTimer -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(gameTimer / 60F);
            int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
            string gameTimerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            gameTimerText.text = gameTimerString;
        } else if (!gameWon && !countDownDisabled)
        {
            gameTimerText.text = "0:00";
            GameTimeOver();
        }


        
    }

    private void GameTimeOver ()
    {
        gameWon = true;
        if(blueTeamTokens == redTeamTokens)
        {
            winText.text = "Teams tied!";
            StartCoroutine(Victory("Teams Tied!"));          
        } else if (blueTeamTokens > redTeamTokens)
        {
            winText.text = "Blue Team Wins!";
            StartCoroutine(Victory("Blue Team Wins!"));
            FMODUnity.RuntimeManager.PlayOneShot(blueWinSound);
        } else if (blueTeamTokens < redTeamTokens)
        {
            winText.text = "Blue Team Wins!";
            StartCoroutine(Victory("Blue Team Wins!"));
            FMODUnity.RuntimeManager.PlayOneShot(redWinSound);
        }
    }

    IEnumerator Victory(string victoryText)
    {
        winPanelImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        foreach(GameObject g in canvasToDisable)
        {
            g.SetActive(false);
        }
        if (cameras)
        { 
        Camera[] cams = cameras.GetComponentsInChildren<Camera>();
        foreach(Camera c in cams)
            {
                c.enabled = false;
            }
        }

        winCam.enabled = true;
        ral.enabled = true;
        vds.StartDisplayEndScreenStats(victoryText);
        yield return new WaitForSeconds(5);
        allowSceneChange = true;
        vds.DisplayContinueButtons();
    }  

    void GoToAwards()
    {       
        StartCoroutine(AwardsFinished());
        winCam.transform.position = targetPosition.position;
        winCam.transform.rotation = targetPosition.rotation;
        awardScene.SetUpAwardScene();
        vds.RemoveScoreboard();
        awardsHeader.SetActive(true);
    }

    IEnumerator AwardsFinished()
    {
        allowSceneChange = false;
        yield return new WaitForSeconds(5);
        allowSceneChange = true;
        rematchButton.SetActive(true);
        wentToAwards = true;
    }

    void OnGUI()
    {
    //    GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());
    }

    public void AButton()
    {
        if (allowSceneChange)
        {

            if (!wentToAwards)
            {
                GoToAwards();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
    }

    public void BButton()
    {
        if (allowSceneChange)
        {
            if (wentToAwards)
            {
                SceneManager.LoadScene("0_MainMenu");
            }
        }
    }
}

