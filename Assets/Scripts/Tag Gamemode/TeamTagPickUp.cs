using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTagPickUp : MonoBehaviour
{
    public float tagTeamNum;
    bool collected;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 500);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision coll)
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
                        Destroy(this.gameObject);
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
}
