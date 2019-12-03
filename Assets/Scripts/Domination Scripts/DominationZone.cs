using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominationZone : MonoBehaviour
{

    public Material plainMat, redMat, blueMat;
    public float checkTime = 1f;
    public DominationManager domManager;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(checkForObjects());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator checkForObjects ()
    {
        int redTeamUnits = 0;
        int blueTeamUnits = 0;
             
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.localScale.x/2f);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Health>() != null)
            {
                if (c.gameObject.GetComponent<Health>().playerNum == 1)
                {
                    blueTeamUnits += 1;
                } else if (c.gameObject.GetComponent<Health>().playerNum == 2)
                {
                    redTeamUnits += 1;
                }                       
            }
        }

        if (hitColliders.Length > 0)
        {
            if(redTeamUnits > blueTeamUnits)
            {
                domManager.redTeamPoints += 1;
                GetComponent<Renderer>().material = redMat;
            } else if (redTeamUnits < blueTeamUnits)
            {
                domManager.blueTeamPoints += 1;
                GetComponent<Renderer>().material = blueMat;
            } else if (redTeamUnits == blueTeamUnits)
            {
                GetComponent<Renderer>().material = plainMat;
            }
        } else
        {
            GetComponent<Renderer>().material = plainMat;
        }
        yield return new WaitForSeconds(checkTime);
        StartCoroutine(checkForObjects());
    }
}
