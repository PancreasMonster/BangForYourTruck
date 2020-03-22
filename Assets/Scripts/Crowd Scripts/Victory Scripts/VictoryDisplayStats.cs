using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryDisplayStats : MonoBehaviour
{
    float imageAlpha = 0;
    public float targetAlpha;
    public float fadeTime;
    public List<Text> playerNames = new List<Text>();
    public List<Text> playerKills = new List<Text>();
    public List<Text> playerDeaths = new List<Text>();
    public Image scoreBoard, winPanel;
    public Text winText;
    public KillManager km;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDisplayEndScreenStats(string victoryText)
    {
        StartCoroutine(DisplayEndScreenStats(victoryText));
    }

    public IEnumerator DisplayEndScreenStats(string victoryText)
    {
        for(int i = 0; i < playerNames.Count; i++)
        {
            playerNames[i].gameObject.SetActive(true);
            if(i >= 1)
                playerNames[i].text = km.players[i-1].transform.name.ToString();
            playerNames[i].color = new Color(playerNames[i].color.r, playerNames[i].color.g, playerNames[i].color.b, 0);
        }

        for(int i = 0; i < playerKills.Count; i++)
        {
            playerKills[i].gameObject.SetActive(true);
            if (i >= 1)
                playerKills[i].text = km.kills[i - 1].ToString();
            playerKills[i].color = new Color(playerKills[i].color.r, playerKills[i].color.g, playerKills[i].color.b, 0);
        }

        for (int i = 0; i < playerDeaths.Count; i++)
        {
            playerDeaths[i].gameObject.SetActive(true);
            if (i >= 1)
                playerDeaths[i].text = km.deaths[i - 1].ToString();
            playerDeaths[i].color = new Color(playerDeaths[i].color.r, playerDeaths[i].color.g, playerDeaths[i].color.b, 0);
        }

        scoreBoard.gameObject.SetActive(true);
        scoreBoard.color = new Color(scoreBoard.color.r, scoreBoard.color.g, scoreBoard.color.b, 0);

        winPanel.gameObject.SetActive(true);
        winPanel.color = new Color(winPanel.color.r, winPanel.color.g, winPanel.color.b, 0);

        winText.text = victoryText;
        winText.color = new Color(winText.color.r, winText.color.g, winText.color.b, 0);

        while (imageAlpha < targetAlpha)
        {
            imageAlpha += 1 * Time.deltaTime / fadeTime;

            yield return null;
        }

        foreach (Text t in playerNames)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, imageAlpha);
        }

        foreach (Text t in playerKills)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, imageAlpha);
        }

        foreach (Text t in playerDeaths)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, imageAlpha);
        }

        scoreBoard.color = new Color(scoreBoard.color.r, scoreBoard.color.g, scoreBoard.color.b, imageAlpha);

        winPanel.color = new Color(winPanel.color.r, winPanel.color.g, winPanel.color.b, imageAlpha);

        winText.color = new Color(winText.color.r, winText.color.g, winText.color.b, imageAlpha);

    }
}
