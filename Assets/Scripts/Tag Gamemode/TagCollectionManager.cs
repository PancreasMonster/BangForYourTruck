using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TagCollectionManager : MonoBehaviour
{
    public Text blueText, redText, winText;
    public Image redImage, blueImage;
    public float blueTeamTokens, redTeamTokens, gameWinningAmount = 30;
    bool gameWon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (!gameWon)
        {
            blueText.text = "Blue Team: " + blueTeamTokens.ToString() + "/" + gameWinningAmount.ToString();
            redText.text = "Red Team: " + redTeamTokens.ToString() + "/" + gameWinningAmount.ToString();
            redImage.fillAmount = redTeamTokens / gameWinningAmount;
            blueImage.fillAmount = blueTeamTokens / gameWinningAmount;
        }

        if (blueTeamTokens >= gameWinningAmount && !gameWon)
        {
            gameWon = true;
            blueText.text = "";
            redText.text = "";
            winText.text = "Blue Team Has Won!";
            StartCoroutine(sceneReload());
        }
        else if (redTeamTokens >= gameWinningAmount && !gameWon)
        {
            gameWon = true;
            blueText.text = "";
            redText.text = "";
            winText.text = "Red Team Has Won!";
            StartCoroutine(sceneReload());
        }
    }

    IEnumerator sceneReload()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());
    }
}

