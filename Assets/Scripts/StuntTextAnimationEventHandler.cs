using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuntTextAnimationEventHandler : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveCrashedState()
    {
        anim.SetBool("Crashed",false);
    }

    public void LeaveGoToScoreTextState()
    {
        anim.SetBool("GoToScoreText", false);
    }
}
