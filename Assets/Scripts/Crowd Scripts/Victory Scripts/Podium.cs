using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Podium : MonoBehaviour
{
    public List<Renderer> cubeColours = new List<Renderer>();
    public List<Color> colors = new List<Color>();
    public GameObject redCar, blueCar;
    public GameObject textItem;
    public KillManager km;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void SetUp(int teamNum, KillManager killManager)
    {
        km = killManager;

        GameObject firstAward;
        int kills = 0;
        int killNum = 0;
        for(int i = 0; i < km.players.Count; i++)
        {
            if(km.kills[i] > kills)
            {
                kills = km.kills[i];
                killNum = i;
            }
        }
        if(km.players[killNum].GetComponent<Health>().teamNum == 1)
        {
            firstAward = Instantiate(blueCar, this.transform);
            firstAward.transform.localPosition = new Vector3(-5, -6, 0);
            GameObject tItem = Instantiate(textItem, firstAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.blue;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Top Fragger";
            tm[2].text = kills.ToString();

        } else
        {
            firstAward = Instantiate(redCar, this.transform);
            firstAward.transform.localPosition = new Vector3(-5, -6, 0);
            GameObject tItem = Instantiate(textItem, firstAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.red;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Top Fragger";
            tm[2].text = kills.ToString();
        }

        GameObject secondAward;
        int deaths = 0;
        int deathNum = 0;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.deaths[i] > deaths)
            {
                deaths = km.deaths[i];
                deathNum = i;
            }
        }
        if (km.players[deathNum].GetComponent<Health>().teamNum == 1)
        {
            secondAward = Instantiate(blueCar, this.transform);
            secondAward.transform.localPosition = new Vector3(-15, 0, 0);
            GameObject tItem = Instantiate(textItem, secondAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.blue;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Team Liability";
            tm[2].text = deaths.ToString();
        }
        else
        {
            secondAward = Instantiate(redCar, this.transform);
            secondAward.transform.localPosition = new Vector3(-15, 0, 0);
            GameObject tItem = Instantiate(textItem, secondAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.red;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Team Liability";
            tm[2].text = deaths.ToString();
        }

        GameObject thirdAward;
        int killSpree = 0;
        int killSpreeNum = 0;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.killSpreeCount[i] > killSpree)
            {
                killSpree = km.kills[i];
                killSpreeNum = i;
            }
        }
        if (km.players[killNum].GetComponent<Health>().teamNum == 1)
        {
            thirdAward = Instantiate(blueCar, this.transform);
            thirdAward.transform.localPosition = new Vector3(-25, 6, 0);
            GameObject tItem = Instantiate(textItem, thirdAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.blue;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Longest Kill Spree";
            tm[2].text = killSpree.ToString();
        }
        else
        {
            thirdAward = Instantiate(redCar, this.transform);
            thirdAward.transform.localPosition = new Vector3(-25, 6, 0);
            GameObject tItem = Instantiate(textItem, thirdAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.red;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Longest Kill Spree";
            tm[2].text = killSpree.ToString();
        } 

        GameObject fourthAward;
        int droneKills = 0;
        int droneKillNum = 0;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.droneKills[i] > droneKills)
            {
                droneKills = km.droneKills[i];
                droneKillNum = i;
            }
        }
        if (km.players[killNum].GetComponent<Health>().teamNum == 1)
        {
            fourthAward = Instantiate(blueCar, this.transform);
            fourthAward.transform.localPosition = new Vector3(-35, 0, 0);
            GameObject tItem = Instantiate(textItem, fourthAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.blue;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Drone Killer";
            tm[2].text = droneKills.ToString();
        }
        else
        {
            fourthAward = Instantiate(redCar, this.transform);
            fourthAward.transform.localPosition = new Vector3(-35, 0, 0);
            GameObject tItem = Instantiate(textItem, fourthAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.red;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Drone Killer";
            tm[2].text = droneKills.ToString();
        }

        GameObject fifthAward;
        int longShotKills = 0;
        int longShotKillNum = 0;
        for (int i = 0; i < km.players.Count; i++)
        {
            if (km.longShotKills[i] > longShotKills)
            {
                longShotKills = km.longShotKills[i];
                longShotKillNum = i;
            }
        }
        if (km.players[killNum].GetComponent<Health>().teamNum == 1)
        {
            fifthAward = Instantiate(blueCar, this.transform);
            fifthAward.transform.localPosition = new Vector3(-45, -6, 0);
            GameObject tItem = Instantiate(textItem, fifthAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.blue;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Sniper";
            tm[2].text = longShotKills.ToString();
        }
        else
        {
            fifthAward = Instantiate(redCar, this.transform);
            fifthAward.transform.localPosition = new Vector3(-45, -6, 0);
            GameObject tItem = Instantiate(textItem, fifthAward.transform);
            tItem.transform.Rotate(0, 180, 0);
            TextMesh[] tm = tItem.GetComponentsInChildren<TextMesh>();
            tm[0].color = Color.blue;
            tm[0].text = km.players[killNum].transform.name;
            tm[1].text = "Sniper";
            tm[2].text = longShotKills.ToString();
        }
    }
}
