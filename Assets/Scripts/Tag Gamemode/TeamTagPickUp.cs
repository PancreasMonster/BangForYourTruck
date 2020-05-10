using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTagPickUp : MonoBehaviour
{
    public float tagTeamNum;
    bool collected;
    public float collectionDelayTime = .75f;
    public GameObject parent;
    public KillManager km;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Rigidbody>().AddForce(Random.insideUnitSphere * 500);
        StartCoroutine(CollectionDelayInitialisation());   
        km = GameObject.Find("KillManager").GetComponent<KillManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (!collected)
        {
            if (coll.transform.tag == "Player")
            {
                TagHolder t = coll.transform.GetComponent<TagHolder>();
                if (coll.transform.GetComponent<Health>().teamNum != tagTeamNum)
                {
                    if (t.currentTags < 3)
                    {
                        km.ScoreFeedCollectToken(coll.gameObject);
                        collected = true;
                        t.AddTag();
                        Destroy(parent);
                    }
                }
                else
                {
                    km.ScoreFeedDeny(coll.gameObject);
                    collected = true;
                    Destroy(parent);
                }
            }
        }
    }

    IEnumerator CollectionDelayInitialisation ()
    {
        yield return new WaitForSeconds(collectionDelayTime);
        this.gameObject.layer = 19;       
    }

}
