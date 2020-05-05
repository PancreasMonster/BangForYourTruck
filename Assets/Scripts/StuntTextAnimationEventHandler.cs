using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuntTextAnimationEventHandler : MonoBehaviour
{
    Animator anim;
    Text[] stuntTexts;
    Text stuntText1;
    Text stuntText2;


    // Start is called before the first frame update
    void Start()
    {
        stuntText1 = transform.GetChild(0).GetComponent<Text>();
        stuntText2 = transform.GetChild(1).GetComponent<Text>();
        anim = GetComponent<Animator>();
        //stuntTexts = GetComponentsInChildren<Text>();
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
        stuntText1.text = "";
        stuntText2.text = "";

        /*foreach(Text t in stuntTexts)
        {
            t.text = "";
        }*/
    }

    public void LeaveGoToScoreTextState()
    {
        //Debug.Log("Leaving GoToScoreState State");
        anim.SetBool("GoToScoreText", false);
        anim.SetTrigger("Idle");
        stuntText1.text = "";
        stuntText2.text = "";

        /*foreach (Text t in stuntTexts)
        {
            t.text = "";
        }*/
    }
}
