using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationEventHandler : MonoBehaviour
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

    public void GoToSinglePlayer()
    {
        anim.SetBool("SinglePlayerMenu", true);
    }

    public void GoToMultiPlayer()
    {
        anim.SetBool("MultiPlayerMenu", true);
    }

    public void LeaveSinglePlayer()
    {
        anim.SetBool("SinglePlayerMenu", false);
    }

    public void LeaveMultiPlayer()
    {
        anim.SetBool("MultiPlayerMenu", false);
    }
}
