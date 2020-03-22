using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TagCollectionManager : MonoBehaviour
{
    public Text winText;
    public Image redImage, blueImage, winPanelImage;
    public float blueTeamTokens, redTeamTokens, gameWinningAmount = 30;
    bool gameWon, allowSceneChange;
    public Camera winCam;
    public RotateAroundLevel ral;
    public List<GameObject> canvasToDisable = new List<GameObject>();
    VictoryDisplayStats vds;


    // Start is called before the first frame update
    void Start()
    {
        vds = GetComponent<VictoryDisplayStats>();
    }

    void Update()
    {
        if (!gameWon)
        {
            redImage.fillAmount = redTeamTokens / gameWinningAmount;
            blueImage.fillAmount = blueTeamTokens / gameWinningAmount;
        }

        if (blueTeamTokens >= gameWinningAmount && !gameWon)
        {
            gameWon = true;
            winText.text = "Blue Team Has Won!";
            StartCoroutine(Victory("Blue Team Has Won!"));
        }
        else if (redTeamTokens >= gameWinningAmount && !gameWon)
        {
            gameWon = true;
            winText.text = "Red Team Has Won!";
            StartCoroutine(Victory("Red Team Has Won!"));
        }

        if(allowSceneChange)
        { 
            for (int i = 0; i < 4; i++)
            {
                if (Input.GetButtonDown("PadA" + (i+1).ToString()))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else if (Input.GetButtonDown("PadB" + (i + 1).ToString()))
                {
                    SceneManager.LoadScene("0_MainMenu");
                }
            }
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
        allowSceneChange = true;
        winCam.enabled = true;
        ral.enabled = true;
        vds.StartDisplayEndScreenStats(victoryText);
    }  

    void OnGUI()
    {
    //    GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());
    }
}

