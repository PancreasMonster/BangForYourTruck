using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagCollectionGate : MonoBehaviour
{
    public List<PlayerBank> playerBanks = new List<PlayerBank>();
    public bool redTeam, blueTeam;
    public TagCollectionManager tagCollectionManager;
    public int gateTeamNum;
    KillManager km;
    AudioSource audio;

    public ParticleSystem left;
    public ParticleSystem right;

    public Animator gridBaseAnim;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        tagCollectionManager = GameObject.Find("TagCollectionManager").GetComponent<TagCollectionManager>();
        km = GameObject.Find("KillManager").GetComponent<KillManager>();
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
                    int i = TH.currentTags;
                    tagCollectionManager.blueTeamTokens += TH.currentTags;
                    foreach (PlayerBank pb in playerBanks)
                    {
                        pb.tagsInBank += TH.currentTags;
                        audio.Play();
                    }
                    TH.currentTags = 0;
                    TH.EmptyTags();
                    left.Play();
                    right.Play();
                    km.ChangeGridColours(km.blueTeamColor);
                    km.ScoreFeedDepositToken(col.gameObject, i);
                }
            } else if (col.transform.tag == "TeamTag" && col.GetComponent<TeamTagPickUp>().tagTeamNum == 2)
            {
                Destroy(col.gameObject);
                gridBaseAnim.SetTrigger("BlueCapture");
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
                    int i = TH.currentTags;
                    tagCollectionManager.redTeamTokens += TH.currentTags;
                    foreach (PlayerBank pb in playerBanks)
                    {
                        pb.tagsInBank += TH.currentTags;
                        audio.Play();
                    }
                    TH.currentTags = 0;
                    TH.EmptyTags();
                    left.Play();
                    km.ChangeGridColours(km.redTeamColor);
                    right.Play();
                    km.ScoreFeedDepositToken(col.gameObject, i);
                }
            }
            else if (col.transform.tag == "TeamTag" && col.GetComponent<TeamTagPickUp>().tagTeamNum == 1)
            {
                Destroy(col.gameObject);
                gridBaseAnim.SetTrigger("RedCapture");
                tagCollectionManager.redTeamTokens++;
                foreach (PlayerBank pb in playerBanks)
                {
                    pb.tagsInBank++;
                }
            }
        } 
    }

    IEnumerator AssignPBs ()
    {
        yield return null;
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
}
