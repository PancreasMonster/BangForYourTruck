using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class GridBaseAnimationManager : MonoBehaviour
{
    KillManager km;
    GameObject[] tcg;

    // Start is called before the first frame update
    void Start()
    {
        km = GameObject.Find("KillManager").GetComponent<KillManager>();
        tcg = GameObject.FindGameObjectsWithTag("CollectionGate");

        km.gridBaseAnim = this.gameObject.GetComponent<Animator>();
        foreach(GameObject gate in tcg)
        {
            TagCollectionGate gateScript = gate.GetComponent<TagCollectionGate>();
            gateScript.gridBaseAnim = this.gameObject.GetComponent<Animator>();
        }
        

    }


}
