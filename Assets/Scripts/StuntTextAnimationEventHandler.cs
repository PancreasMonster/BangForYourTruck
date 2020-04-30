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
        Debug.Log("Leaving Crashed State");
        anim.SetBool("Crashed", false);
        anim.SetTrigger("Idle");
        foreach(Text t in stuntStrings)
        {
            t.text = "";
        }
    }

    public void LeaveGoToScoreTextState()
    {
        Debug.Log("Leaving GoToScoreState State");
        anim.SetBool("GoToScoreText", false);
        anim.SetTrigger("Idle");
        foreach (Text t in stuntStrings)
        {
            t.text = "";
        }
    }
}
