using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardScene : MonoBehaviour
{
    public GameObject panelTop, panelBottom;
    public KillManager km;
    public Color blueTeamColor, redTeamColor;
    public GameObject textItem;
    public Sprite killImage, deathImage, killSpreeImage, droneKillerImage, longShotImage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpAwardScene()
    {
        
        GameObject firstAward;
        int kills = 0;
        int killNum = -1;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.kills[i] > kills)
            {
                kills = km.kills[i];
                killNum = i;
            }
        }


        firstAward = Instantiate(textItem, panelTop.transform);
        AwardSceneHolder aSH = firstAward.GetComponent<AwardSceneHolder>();
        aSH.background.sprite = killImage;
        if (killNum == -1)
        {
            aSH.playerName.text = "";
        }
        else
        {
            if (km.players[killNum].GetComponent<Health>().teamNum == 1)
                aSH.playerName.color = blueTeamColor;
            else
                aSH.playerName.color = redTeamColor;

            aSH.playerName.text = km.players[killNum].transform.name;
        }
            
        aSH.title.text = "Top Fragger";
        aSH.detail.text = "Kills: ";
        aSH.amount.text = kills.ToString();



        GameObject secondAward;
        int deaths = 0;
        int deathNum = -1;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.deaths[i] > deaths)
            {
                deaths = km.deaths[i];
                deathNum = i;
            }
        }

        secondAward = Instantiate(textItem, panelTop.transform);
        AwardSceneHolder aSH2 = secondAward.GetComponent<AwardSceneHolder>();
        aSH2.background.sprite = deathImage;
        if (deathNum == -1)
        {
            aSH2.playerName.text = "";
        }
        else
        {
            if (km.players[deathNum].GetComponent<Health>().teamNum == 1)
                aSH2.playerName.color = blueTeamColor;
            else
                aSH2.playerName.color = redTeamColor;

            aSH2.playerName.text = km.players[deathNum].transform.name;
        }
        aSH2.title.text = "Team Liability";
        aSH2.detail.text = "Deaths: ";
        aSH2.amount.text = deaths.ToString();
         
        GameObject thirdAward;
        int killSpree = 0;
        int killSpreeNum = -1;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.killSpreeCount[i] > killSpree)
            {
                killSpree = km.kills[i];
                killSpreeNum = i;
            }
        }

        thirdAward = Instantiate(textItem, panelTop.transform);
        AwardSceneHolder aSH3 = thirdAward.GetComponent<AwardSceneHolder>();
        aSH3.background.sprite = killSpreeImage;
        if (killSpreeNum == -1)
        {
            aSH3.playerName.text = "";
        }
        else
        {
            if (km.players[killSpreeNum].GetComponent<Health>().teamNum == 1)
                aSH3.playerName.color = blueTeamColor;
            else
                aSH3.playerName.color = redTeamColor;

            aSH3.playerName.text = km.players[killSpreeNum].transform.name;
        }
        aSH3.title.text = "Rampager";
        aSH3.detail.text = "Kill Spree: ";
        aSH3.amount.text = killSpree.ToString();


        GameObject fourthAward;
        int droneKills = 0;
        int droneKillNum = -1;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.droneKills[i] > droneKills)
            {
                droneKills = km.droneKills[i];
                droneKillNum = i;
            }
        }

        fourthAward = Instantiate(textItem, panelBottom.transform);
        AwardSceneHolder aSH4 = fourthAward.GetComponent<AwardSceneHolder>();
        aSH4.background.sprite = droneKillerImage;
        if (droneKillNum == -1)
        {
            aSH4.playerName.text = "";
        }
        else
        {
            if (km.players[droneKillNum].GetComponent<Health>().teamNum == 1)
                aSH4.playerName.color = blueTeamColor;
            else
                aSH4.playerName.color = redTeamColor;

            aSH4.playerName.text = km.players[droneKillNum].transform.name;
        }
        aSH4.title.text = "Drone Killer";
        aSH4.detail.text = "Drones Killed: ";
        aSH4.amount.text = droneKills.ToString();

        GameObject fifthAward;
        int longShotKills = 0;
        int longShotKillNum = -1;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.longShotKills[i] > longShotKills)
            {
                longShotKills = km.longShotKills[i];
                longShotKillNum = i;
            }
        }

        fifthAward = Instantiate(textItem, panelBottom.transform);
        AwardSceneHolder aSH5 = fifthAward.GetComponent<AwardSceneHolder>();
        aSH5.background.sprite = longShotImage;
        if (longShotKillNum == -1)
        {
            aSH5.playerName.text = "";
        }
        else
        {
            if (km.players[longShotKillNum].GetComponent<Health>().teamNum == 1)
                aSH5.playerName.color = blueTeamColor;
            else
                aSH5.playerName.color = redTeamColor;

            aSH5.playerName.text = km.players[longShotKillNum].transform.name;
        }
        aSH5.title.text = "Sniper";
        aSH5.detail.text = "Long Shots: ";
        aSH5.amount.text = longShotKills.ToString();

        panelTop.SetActive(true);
        panelBottom.SetActive(true);

    }
}
