using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryDisplayStats : MonoBehaviour
{
    float imageAlpha = 0;
    public float targetAlpha;
    public float fadeTime;
    public Image scoreBoard, winPanel;
    public Text winText;
    public KillManager km;
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> victoryItems = new List<GameObject>();
    public GameObject victoryScreenItem;
    public GameObject victoryScreenItemHolder, victoryScreenHeaderHolder, victoryScreenItemHolder2;
    public GameObject continueButtons;
    public bool gameVictory = false;

    // Start is called before the first frame update
    void Start()
    {
        players = km.players;
        for (int num = 0; num < players.Count; num++)
        {
            GameObject VSI = Instantiate(victoryScreenItem, victoryScreenItemHolder.transform);
            Text[] texts = new Text[6];
            texts = VSI.GetComponentsInChildren<Text>();

            texts[0].text = players[num].transform.name.ToString();
            texts[1].text = km.kills[num].ToString();
            texts[2].text = km.deaths[num].ToString();
            texts[3].text = km.tagsDeposited[num].ToString();
            texts[4].text = km.tagsDenied[num].ToString();
            texts[5].text = km.score[num].ToString();

            victoryItems.Add(VSI);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        for (int num = 0; num < players.Count; num++)
        {

            Text[] texts = new Text[6];
            texts = victoryItems[num].GetComponentsInChildren<Text>();

            texts[0].text = players[num].transform.name.ToString();
            texts[1].text = km.kills[num].ToString();
            texts[2].text = km.deaths[num].ToString();
            texts[3].text = km.tagsDeposited[num].ToString();
            texts[4].text = km.tagsDenied[num].ToString();
            texts[5].text = km.score[num].ToString();

        }
    }

    public void StartDisplayEndScreenStats(string victoryText)
    {
        
        DisplayEndScreenStats(victoryText);
    }

    public void DisplayEndScreenStats(string victoryText)
    {

        foreach(GameObject g in km.players)
        {
            g.GetComponent<PlayerPause>().noPlayerInput = true;
        }

        gameVictory = true;

        UpdateScore();

        scoreBoard.gameObject.SetActive(true);

        victoryScreenItemHolder.gameObject.SetActive(true);

        victoryScreenItemHolder2.gameObject.SetActive(true);

        victoryScreenHeaderHolder.gameObject.SetActive(true);

        winPanel.gameObject.SetActive(true);

        winText.text = victoryText;
       
    }

    public void DisplayContinueButtons ()
    {
        continueButtons.SetActive(true);
    }

    public void RemoveScoreboard()
    {
        scoreBoard.gameObject.SetActive(false);

        victoryScreenItemHolder.gameObject.SetActive(false);

        victoryScreenItemHolder2.gameObject.SetActive(false);

        victoryScreenHeaderHolder.gameObject.SetActive(false);

        winPanel.gameObject.SetActive(false);

        continueButtons.SetActive(false);
    }

    public void DisableInput()
    {
        foreach (GameObject g in km.players)
        {
            g.GetComponent<PlayerPause>().noPlayerInput = true;
        }
    }

    public void EnableInput()
    {
        foreach (GameObject g in km.players)
        {
            g.GetComponent<PlayerPause>().noPlayerInput = false;
        }
    }
}
