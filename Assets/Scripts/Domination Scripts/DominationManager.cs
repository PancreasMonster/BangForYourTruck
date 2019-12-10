using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DominationManager : MonoBehaviour
{
    public float redTeamPoints;
    public float blueTeamPoints;
    public Text blueText, redText, winText;
    public Image redImage, blueImage;
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
            blueText.text = "Blue Team: " + blueTeamPoints.ToString() + "/" + winAmount.ToString();
            redText.text = "Red Team: " + redTeamPoints.ToString() + "/" + winAmount.ToString();
            redImage.fillAmount = redTeamPoints / winAmount;
            blueImage.fillAmount = blueTeamPoints / winAmount;
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
