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
    bool gameWon, allowSceneChange;
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


    // Start is called before the first frame update
    void Start()
    {
        vds = GetComponent<VictoryDisplayStats>();
        awardScene = GetComponent<AwardScene>();
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
            winText.text = "Blue Team Has Won!";
            StartCoroutine(Victory("Blue Team Has Won!"));
            FMODUnity.RuntimeManager.PlayOneShot(blueWinSound);
            blueTeamWon = true;
        }
        else if (redTeamTokens >= gameWinningAmount && !gameWon)
        {
            gameWon = true;
            winText.text = "Red Team Has Won!";
            StartCoroutine(Victory("Red Team Has Won!"));
            FMODUnity.RuntimeManager.PlayOneShot(redWinSound);
            redTeamWon = true;
        }

        if(allowSceneChange)
        { 
            for (int i = 0; i < 4; i++)
            {
                if (Input.GetButtonDown("PadA" + (i+1).ToString()))
                {
                    GoToAwards();
                }
                else if (Input.GetButtonDown("PadB" + (i + 1).ToString()))
                {
                    SceneManager.LoadScene("0_MainMenu");
                }
            }
        }

        if(gameTimer >= 0 && !gameWon)
        {
            gameTimer -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(gameTimer / 60F);
            int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
            string gameTimerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            gameTimerText.text = gameTimerString;
        } else if (!gameWon)
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
            StartCoroutine(Victory("Teams tied!"));          
        } else if (blueTeamTokens > redTeamTokens)
        {
            winText.text = "Blue Team Has Won!";
            StartCoroutine(Victory("Blue Team Has Won!"));
            FMODUnity.RuntimeManager.PlayOneShot(blueWinSound);
        } else if (blueTeamTokens < redTeamTokens)
        {
            winText.text = "Red Team Has Won!";
            StartCoroutine(Victory("Red Team Has Won!"));
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
        
        winCam.enabled = true;
        ral.enabled = true;
        vds.StartDisplayEndScreenStats(victoryText);
        yield return new WaitForSeconds(5);
        allowSceneChange = true;
        vds.DisplayContinueButtons();
        GoToAwards();
    }  

    void GoToAwards()
    {
        ral.enabled = false;
        winCam.transform.position = targetPosition.position;
        winCam.transform.rotation = targetPosition.rotation;
        if (blueTeamWon)
        {
            awardScene.SetUpPodium(1, km);
        } else
        {
            awardScene.SetUpPodium(2, km);
        }
        vds.RemoveScoreboard();
    }

    void OnGUI()
    {
    //    GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());
    }
}

