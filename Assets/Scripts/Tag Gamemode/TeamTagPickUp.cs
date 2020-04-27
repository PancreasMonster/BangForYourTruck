using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTagPickUp : MonoBehaviour
{
    public float tagTeamNum;
    bool collected;
    public float collectionDelayTime = .75f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Rigidbody>().AddForce(Random.insideUnitSphere * 500);
        StartCoroutine(CollectionDelayInitialisation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionTrigger(Collision coll)
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
                        collected = true;
                        t.AddTag();
                        Destroy(this.transform.parent);
                    }
                }
                else
                {
                    collected = true;
                    Destroy(this.gameObject);
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
