using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public float damageToDeal;
    float closestDistance;
    public float range;
    public bool hitAlready;

    List<GameObject> targets = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void ChainLightningEffect() {

        GetComponent<Health>().health -= damageToDeal;

        //List<float> rangesToTargets = new List<float>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Health>() != null && c.gameObject != this.gameObject)
            {
                if (c.gameObject.GetComponent<Health>().playerNum != GetComponentInParent<Health>().playerNum 
                    && c.gameObject.GetComponent<ChainLightning>().hitAlready == false)
                {
                    targets.Add(c.gameObject);
                    if (targets.Count == 1)
                    {
                        closestDistance = Vector3.Distance(transform.position, c.gameObject.transform.position);

                    } else {

                        float distance = Vector3.Distance(transform.position, c.gameObject.transform.position);

                        if (distance < closestDistance) {

                            targets.RemoveAt(0);
                        }
                    }                    
                }
            }

            // one target left deal them damage, then feed in dmageToDeal*.75 & range*.75

        }
    }    
}
