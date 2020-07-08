using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavernsGridBaseAnimationManager : MonoBehaviour
{
    KillManager km;
    GameObject[] tcg;
    GridFloorBeatVisualiser[] gfbv;
    bool off;

    // Start is called before the first frame update
    void Start()
    {
        off = false;
        km = GameObject.Find("KillManager").GetComponent<KillManager>();
        tcg = GameObject.FindGameObjectsWithTag("CollectionGate");
        gfbv = GetComponentsInChildren<GridFloorBeatVisualiser>();
        km.gridBaseAnim = this.gameObject.GetComponent<Animator>();
        foreach (GameObject gate in tcg)
        {
            TagCollectionGate gateScript = gate.GetComponent<TagCollectionGate>();
            gateScript.gridBaseAnim = this.gameObject.GetComponent<Animator>();
        }
    }

    public void ToggleGridFloorBeatVisualiser()
    {
        if (!off)
        {
            foreach (GridFloorBeatVisualiser x in gfbv) 
            {
                x.enabled = false;
            }
            off = true;
        }
        else
        {
            foreach (GridFloorBeatVisualiser x in gfbv)
            {
                x.enabled = true;
            }
            off = false;
        }
    }
}
