using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagCollectionGate : MonoBehaviour
{
    public List<PlayerBank> playerBanks = new List<PlayerBank>();
    public bool redTeam, blueTeam;
    public TagCollectionManager tagCollectionManager;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(blueTeam)
        {
            if(col.transform.tag == "Player")
            {
                TagHolder TH = col.GetComponent<TagHolder>();
                if(TH.currentTags > 0)
                {
                    tagCollectionManager.blueTeamTokens += TH.currentTags;
                    foreach (PlayerBank pb in playerBanks)
                    {
                        pb.tagsInBank += TH.currentTags;
                    }
                    TH.currentTags = 0;                   
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
            if (col.transform.tag == "Player")
            {
                TagHolder TH = col.GetComponent<TagHolder>();
                if (TH.currentTags > 0)
                {
                    tagCollectionManager.redTeamTokens += TH.currentTags;
                    foreach (PlayerBank pb in playerBanks)
                    {
                        pb.tagsInBank += TH.currentTags;
                    }
                    TH.currentTags = 0;
                }
            }
            else if (col.transform.tag == "TeamTag" && col.GetComponent<TeamTagPickUp>().tagTeamNum == 2)
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
