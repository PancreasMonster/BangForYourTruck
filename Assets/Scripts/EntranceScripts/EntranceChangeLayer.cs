using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceChangeLayer : MonoBehaviour
{
    public string layerName;
    public bool turnOffBoost;
    public LayerMask lm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.transform.tag == "Player")
        {
            coll.gameObject.layer = LayerMask.NameToLayer(layerName);
            coll.GetComponent<FlipOver>().layer = lm;
            ChangeLayersRecursively(coll.transform, layerName);
            if(turnOffBoost)
            {
                coll.gameObject.GetComponent<PropelSelf>().canBoost = false;
                coll.gameObject.GetComponent<PlayerAutoLauncher>().StopLaunch();
            } else
            {
                coll.gameObject.GetComponent<PropelSelf>().canBoost = true;
                ThrowableCooldown tc = coll.GetComponent<BuildModeFire>().discUIImages[0].gameObject.GetComponent<ThrowableCooldown>();
                tc.GoOnCooldown(tc.fillAmountValue);
            }
        }
    }


    private void ChangeLayersRecursively(Transform trans, string name)
    {
        foreach (Transform child in trans)
        {
            child.gameObject.layer = LayerMask.NameToLayer(name);
            ChangeLayersRecursively(child, name);
        }
    }
}
