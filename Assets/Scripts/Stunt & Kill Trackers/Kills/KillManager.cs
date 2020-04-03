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
    public int killSpreeNum = 3;

    public KillFeed kf;

    public List<PlayerInput> PI = new List<PlayerInput>();

    public PlayerInputManager pim;

    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(startSound, players[0].transform.position);

    }

    

    // Update is called once per frame
    void Update()
    {
        

        int conTrollerCount = 0;

    string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if (names[x].Length == 19)
            {
                print("PS4 CONTROLLER IS CONNECTED");
                conTrollerCount++;
            }
            if (names[x].Length == 33)
            {
                print("XBOX ONE CONTROLLER IS CONNECTED");
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
