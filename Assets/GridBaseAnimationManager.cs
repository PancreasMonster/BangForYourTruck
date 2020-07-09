using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GridBaseAnimationManager : MonoBehaviour
{
    KillManager km;
    GameObject[] tcg;
    GridFloorBeatVisualiser base3;
    bool off;

    // Start is called before the first frame update
    void Start()
    {
        off = false;
        tcg = GameObject.FindGameObjectsWithTag("CollectionGate");
        base3 = GetComponentInChildren<GridFloorBeatVisualiser>();
        KillManager.instance.gridBaseAnim = this.gameObject.GetComponent<Animator>();
        KillManager.instance.Test();
        foreach(GameObject gate in tcg)
        {
            TagCollectionGate gateScript = gate.GetComponent<TagCollectionGate>();
            gateScript.gridBaseAnim = this.gameObject.GetComponent<Animator>();
        } 
    }

    public void ToggleGridFloorBeatVisualiser()
    {
        if (!off) 
        {
            base3.enabled = false;
            off = true;
        } 
        else 
        {
            base3.enabled = true;
            off = false;
        }
    }

}
