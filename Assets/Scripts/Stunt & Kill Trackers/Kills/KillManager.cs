using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int killSpreeNum = 3;

    public KillFeed kf;

    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(startSound, players[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KillTracked (GameObject killer, GameObject victim, string damageString)
    {
        int deathNum = victim.GetComponent<Health>().playerNum - 1;
        deaths[deathNum]++;
        killSpree[deathNum] = 0;
        int killNum = killer.GetComponent<Health>().playerNum - 1;
        kills[killNum]++;
        killSpree[killNum]++;
        DoubleKillDialougeActivation(killNum);
        killSpreeDialougeActivation(killNum);
        StartCoroutine(DoubleKillDialougeBool(killNum));
        kf.KillAnnouncement(killer, victim, damageString);
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

    IEnumerator DoubleKillDialougeBool (int i)
    {
        doubleKill[i] = true;
        yield return new WaitForSeconds(doubleKillTimer);
        doubleKill[i] = false;
    }

}
