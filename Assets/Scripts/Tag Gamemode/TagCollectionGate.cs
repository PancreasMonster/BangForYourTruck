using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagCollectionGate : MonoBehaviour
{
    public List<PlayerBank> playerBanks = new List<PlayerBank>();
    public bool redTeam, blueTeam;
    public TagCollectionManager tagCollectionManager;
    public int gateTeamNum;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        tagCollectionManager = GameObject.Find("TagCollectionManager").GetComponent<TagCollectionManager>();
        if (redTeam)
        {
            for (int i = 0; i < playerBanks.Count; i++)
            {
                playerBanks[i] = GameObject.Find("New Trucks").transform.Find("RedTeam").transform.Find("Player" + (i + 1).ToString()).GetComponent<PlayerBank>();
            }
        }
        else if (blueTeam)
        {
            for (int i = 0; i < playerBanks.Count; i++)
            {
                playerBanks[i] = GameObject.Find("New Trucks").transform.Find("BlueTeam").transform.Find("Player" + (i + 1).ToString()).GetComponent<PlayerBank>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(blueTeam)
        {
            if(col.transform.tag == "Player" && col.GetComponent<Health>().teamNum == gateTeamNum)
            {
                TagHolder TH = col.GetComponent<TagHolder>();
                if(TH.currentTags > 0)
                {
                    tagCollectionManager.blueTeamTokens += TH.currentTags;
                    foreach (PlayerBank pb in playerBanks)
                    {
                        pb.tagsInBank += TH.currentTags;
                        audio.Play();
                    }
                    TH.currentTags = 0;
                    TH.EmptyTags();
                }
            } else if (col.transform.tag == "TeamTag" && col.GetComponent<TeamTagPickUp>().tagTeamNum == 2)
            {
                Destroy(col.gameObject);
                tagCollectionManager.blueTeamTokens++;
                foreach (PlayerBank pb in playerBanks)
                {
                    pb.tagsInBank++;
                }
            }
        } else if (redTeam)
        {
            if (col.transform.tag == "Player" && col.GetComponent<Health>().teamNum == gateTeamNum)
            {
                TagHolder TH = col.GetComponent<TagHolder>();
                if (TH.currentTags > 0)
                {
                    tagCollectionManager.redTeamTokens += TH.currentTags;
                    foreach (PlayerBank pb in playerBanks)
                    {
                        pb.tagsInBank += TH.currentTags;
                        audio.Play();
                    }
                    TH.currentTags = 0;
                    TH.EmptyTags();
                }
            }
            else if (col.transform.tag == "TeamTag" && col.GetComponent<TeamTagPickUp>().tagTeamNum == 1)
            {
                Destroy(col.gameObject);
                tagCollectionManager.redTeamTokens++;
                foreach (PlayerBank pb in playerBanks)
                {
                    pb.tagsInBank++;
                }
            }
        }
    }
}
