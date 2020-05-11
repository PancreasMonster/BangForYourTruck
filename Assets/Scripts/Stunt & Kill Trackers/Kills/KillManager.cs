using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillManager : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string startSound;

    [FMODUnity.EventRef]
    public string doubleKillDialouge;

    [FMODUnity.EventRef]
    public string killSpreeDialouge;

    public List<bool> doubleKill = new List<bool>();
    public float doubleKillTimer;
    public List<GameObject> players = new List<GameObject>();
    public List<int> kills = new List<int>();
    public List<int> deaths = new List<int>();
    public List<int> killSpree = new List<int>();
    public List<int> killSpreeCount = new List<int>();
    public List<int> droneKills = new List<int>();
    public List<int> longShotKills = new List<int>();
    public List<int> tagsDeposited = new List<int>();
    public List<int> tagsDenied = new List<int>();
    public List<int> score = new List<int>();
    public int killSpreeNum = 3;

    public KillFeed kf;

    public List<PlayerInput> PI = new List<PlayerInput>();

    public PlayerInputManager pim;

    public GameObject stadiumPyrotechnicParticles;

    public float longShotDistance = 300;

    public TrainingManager tm;

    // Start is called before the first frame update
    void Start()
    {
        if(!tm)
        FMODUnity.RuntimeManager.PlayOneShot(startSound, players[0].transform.position);

    }

    

    // Update is called once per frame
    void Update()
    {
        

        int conTrollerCount = 0;

    string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 33)
            {
                
                //set a controller bool to true
                conTrollerCount++;

            }
        }


        Debug.Log(conTrollerCount);
    
        for (int i = 0; i < PI.Count; i++)
        {
            if((i+1) <= conTrollerCount)
            {
                PI[i].enabled = true;
            } else
            {
                PI[i].enabled = false;
            }
        }

    }

    public void KillTracked (GameObject killer, GameObject victim, Sprite sourceImage, int teamNumV, int teamNumK)
    {
        int deathNum = victim.GetComponent<Health>().playerNum - 1;

        if (victim.GetComponent<Health>().teamNum == 1)
        {
            stadiumPyrotechnicParticles.GetComponent<PryotechnicsManager>().BlueTeamParticlesFire();
        }
        else
        {
            stadiumPyrotechnicParticles.GetComponent<PryotechnicsManager>().RedTeamParticlesFire();
        }

        deaths[deathNum]++;
        killSpree[deathNum] = 0;
        int killNum = killer.GetComponent<Health>().playerNum - 1;
        kills[killNum]++;
        killSpree[killNum]++;
        if (killSpree[killNum] > killSpreeCount[killNum])
            killSpreeCount[killNum] = killSpree[killNum];
        killSpreeDialougeActivation(killNum);
        
        kf.KillAnnouncement(killer, victim, sourceImage, teamNumV, teamNumK);
        int longShot = 0;
        if (Vector3.Distance(killer.transform.position, victim.transform.position) > longShotDistance)
            longShot = 1;
        ScoreFeedTotalMessage(killer, killNum, longShot);
        DoubleKillDialougeActivation(killNum);
        StartCoroutine(DoubleKillDialougeBool(killNum));
    }

    private void DoubleKillDialougeActivation (int i)
    {
        if(doubleKill[i] == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(doubleKillDialouge);
        }
    }

    private void killSpreeDialougeActivation(int i)
    {
        if (killSpree[i] >= killSpreeNum)
        {
            FMODUnity.RuntimeManager.PlayOneShot(killSpreeDialouge);
        }
    }

    public void ScoreFeedTotalMessage(GameObject killer, int i, int ls)
    {
        List<string> flavourTexts = new List<string>();
        List<int> scoreList = new List<int>();
        flavourTexts.Add("Kill");
        scoreList.Add(100);
        score[i] += 100;
        int killCount = -1;
        foreach(int k in kills)
        {
            killCount += k;
        }
        if (killCount == 0)
        {
            string fb = "First Blood";
            flavourTexts.Add(fb);
            scoreList.Add(75);
            score[i] += 75;
        }
        if (doubleKill[i] == true)
        {

            string dk = "Double Kill";
            flavourTexts.Add(dk);
            scoreList.Add(75);
            score[i] += 75;
        }
        if (killSpree[i] >= killSpreeNum)
        {
            string ks = "Kill Spree";
            flavourTexts.Add(ks);
            scoreList.Add(75);
            score[i] += 75;
        }
        if (ls == 1)
        {
            string lss = "Long Shot";
            flavourTexts.Add(lss);
            scoreList.Add(50);
            longShotKills[i]++;
            score[i] += 75;
        }
        if(killer.GetComponent<FlipOver>().timer > killer.GetComponent<FlipOver>().timerAllowance)
        {
            string aerial = "Aerial Kill";
            flavourTexts.Add(aerial);
            scoreList.Add(75);
            score[i] += 75;
        }
        Debug.Log(flavourTexts.Count);
        killer.GetComponent<TextPopUp>().ScoreFeedMessage(flavourTexts, scoreList);
    }

    public void ScoreFeedDrift(GameObject player, int driftLength)
    {
        List<string> flavourTexts = new List<string>();
        List<int> scoreList = new List<int>();
        int i = player.GetComponent<Health>().playerNum - 1;
        if (driftLength > 100)
        {
            flavourTexts.Add("Long Drift");
            scoreList.Add(100);
            score[i] += 100;
            if (tm)
                tm.Drift();
        } else if (driftLength > 50)
        {
            flavourTexts.Add("Short Drift");
            scoreList.Add(50);
            score[i] += 50;
            if (tm)
                tm.Drift();
        } else if (driftLength >= 25)
        {
            if (tm)
                tm.Drift();
            flavourTexts.Add("Mini Drift");
            scoreList.Add(25);
            score[i] += 25;
        }


        player.GetComponent<TextPopUp>().ScoreFeedMessage(flavourTexts, scoreList);
    }

    public void ScoreFeedStunt(GameObject player, List<string> stunts, List<int> scores)
    {
        List<string> flavourTexts = new List<string>();
        List<int> scoreList = new List<int>();
        int i = player.GetComponent<Health>().playerNum - 1;

        foreach (string s in stunts)
        {
            flavourTexts.Add(s);            
        }

        foreach (int s in scores)
        {
            scoreList.Add(s);
            score[i] += s;
        }

        player.GetComponent<TextPopUp>().ScoreFeedMessage(flavourTexts, scoreList);
    }

    public void ScoreFeedDepositToken(GameObject player, int tokens)
    {
        List<string> flavourTexts = new List<string>();
        List<int> scoreList = new List<int>();
        if(tokens == 1)
        flavourTexts.Add("Tag Deposited");
        else
        flavourTexts.Add("Tags Deposited x" + tokens);
        scoreList.Add(100 * tokens);
        player.GetComponent<TextPopUp>().ScoreFeedMessage(flavourTexts, scoreList);
        int i = player.GetComponent<Health>().playerNum - 1;
        tagsDeposited[i]++;
        score[i] += 50;
    }

    public void ScoreFeedCollectToken(GameObject player)
    {
        List<string> flavourTexts = new List<string>();
        List<int> scoreList = new List<int>();
        flavourTexts.Add("Tag Collected");
        scoreList.Add(50);
        player.GetComponent<TextPopUp>().ScoreFeedMessage(flavourTexts, scoreList);
        int i = player.GetComponent<Health>().playerNum - 1;
        score[i] += 50;
    }

    public void ScoreFeedDeny(GameObject player)
    {
        List<string> flavourTexts = new List<string>();
        List<int> scoreList = new List<int>();
        flavourTexts.Add("Tag Denied");
        scoreList.Add(50);
        player.GetComponent<TextPopUp>().ScoreFeedMessage(flavourTexts, scoreList);
        int i = player.GetComponent<Health>().playerNum - 1;
        tagsDenied[i]++;
        score[i] += 50;
    }

    IEnumerator DoubleKillDialougeBool(int i)
    {
        doubleKill[i] = true;
        yield return new WaitForSeconds(doubleKillTimer);
        doubleKill[i] = false;
    }
}
