using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuntTextAnimationEventHandler : MonoBehaviour
{
    Animator anim;
    Text[] stuntStrings;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        stuntStrings = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveCrashedState()
    {
        anim.SetBool("Crashed",false);
        foreach(Text t in stuntStrings)
        {
            t.text = "";
        }
    }

    public void LeaveGoToScoreTextState()
    {
        anim.SetBool("GoToScoreText", false);
        foreach (Text t in stuntStrings)
        {
            t.text = "";
        }
    }
}
