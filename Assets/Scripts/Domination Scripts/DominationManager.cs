using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DominationManager : MonoBehaviour
{
    public int redTeamPoints;
    public int blueTeamPoints;
    public Text blueText, redText, winText;
    public int winAmount = 200;
    bool gameWon;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameWon)
        {
            blueText.text = "Blue Team: " + blueTeamPoints.ToString();
            redText.text = "Red Team: " + redTeamPoints.ToString();
        }

        if(blueTeamPoints > winAmount && !gameWon)
        {
            gameWon = true;
            blueText.text = "";
            redText.text = "";
            winText.text = "Blue Team Has Won!";
            StartCoroutine(sceneReload());
        } else if (redTeamPoints > winAmount && !gameWon)
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
}
