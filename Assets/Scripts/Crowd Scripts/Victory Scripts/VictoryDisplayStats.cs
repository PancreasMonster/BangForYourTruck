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
    public GameObject victoryScreenItem;
    public GameObject victoryScreenItemHolder, victoryScreenHeaderHolder;

    // Start is called before the first frame update
    void Start()
    {
        players = km.players;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDisplayEndScreenStats(string victoryText)
    {
        
        DisplayEndScreenStats(victoryText);
    }

    public void DisplayEndScreenStats(string victoryText)
    {
        for(int num = 0; num < players.Count; num++)
        {
                GameObject VSI = Instantiate(victoryScreenItem, victoryScreenItemHolder.transform);
                Text[] texts = new Text[4];
                texts = VSI.GetComponentsInChildren<Text>();
                texts[0].text = players[num].transform.name.ToString();
                texts[1].text = km.kills[num].ToString();
                texts[2].text = km.deaths[num].ToString();
                texts[3].text = Mathf.CeilToInt(players[num].GetComponent<StuntChecker>().score).ToString();   
        }

        scoreBoard.gameObject.SetActive(true);

        victoryScreenItemHolder.gameObject.SetActive(true);

        victoryScreenHeaderHolder.gameObject.SetActive(true);

        winPanel.gameObject.SetActive(true);

        winText.text = victoryText;
       
    }
}
