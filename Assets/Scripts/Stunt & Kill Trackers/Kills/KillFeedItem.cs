using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillFeedItem : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void Setup (GameObject killer, GameObject victim, string damageString)
    {
        if (damageString != null)
            text.text = "<b>" + killer.transform.name + "</b>" + " " + damageString + " " + "<b>" + victim.transform.name + "</b>";
        else
            text.text = "<b>" + killer.transform.name + "</b>" + " " + "Killed" + "<b>" + victim.transform.name + "</b>";
    }
}
