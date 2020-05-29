using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralCollectionGate : MonoBehaviour
{
    public List<PlayerBank> playerBanks = new List<PlayerBank>();
    public TagCollectionManager tagCollectionManager;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        tagCollectionManager = GameObject.Find("TagCollectionManager").GetComponent<TagCollectionManager>();
        //StartCoroutine(AssignPBs());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.transform.tag == "Player" && col.GetComponent<Health>().teamNum == 1)
        {
            TagHolder TH = col.GetComponent<TagHolder>();
            if (TH.currentTags > 0)
            {
                tagCollectionManager.blueTeamTokens += TH.currentTags;
                for (int i = 0; i < playerBanks.Count; i++)
                {
                    if (i == 2 && i < playerBanks.Count + 1)
                        playerBanks[i].tagsInBank += TH.currentTags;
                        audio.Play();
                }
                TH.currentTags = 0;
                TH.EmptyTags();
            }
        }
        else if (col.transform.tag == "Player" && col.GetComponent<TeamTagPickUp>().tagTeamNum == 2)
        {
            Destroy(col.gameObject);
            tagCollectionManager.blueTeamTokens++;
            for (int i = 0; i < playerBanks.Count; i++)
            {
                if (i == 2 && i < playerBanks.Count + 1)
                    playerBanks[i].tagsInBank ++;            
            }
        }

        if (col.transform.tag == "Player" && col.GetComponent<Health>().teamNum == 2)
        {
            TagHolder TH = col.GetComponent<TagHolder>();
            if (TH.currentTags > 0)
            {
                tagCollectionManager.redTeamTokens += TH.currentTags;
                for (int i = 0; i < playerBanks.Count; i++)
                {
                    if (i < 2)
                        playerBanks[i].tagsInBank += TH.currentTags;
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
            for (int i = 0; i < playerBanks.Count; i++)
            {
                if (i < 2)
                    playerBanks[i].tagsInBank++;
            }
        }

    }

    IEnumerator AssignPBs()
    {
        yield return null;
       
            for (int i = 0; i < playerBanks.Count; i++)
            {
                if(i < 2)
                playerBanks[i] = GameObject.Find("New Trucks").transform.Find("RedTeam").transform.Find("Player" + (i + 1).ToString()).GetComponent<PlayerBank>();
                if(i == 2 &&  i < playerBanks.Count + 1)
                playerBanks[i] = GameObject.Find("New Trucks").transform.Find("BlueTeam").transform.Find("Player" + (i - 1).ToString()).GetComponent<PlayerBank>();
        }
        
            
        
    }
}
