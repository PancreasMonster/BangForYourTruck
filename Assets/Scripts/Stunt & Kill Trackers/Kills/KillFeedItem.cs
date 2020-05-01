using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillFeedItem : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void Setup (GameObject killer, GameObject victim, string damageString, int teamNumV, int teamNumK)
    {
        if (teamNumV == 1 && teamNumK == 1)
        {
            if (damageString != null)
                text.text = "<b>" + "<Color=#849dd6>" + killer.transform.name + "</Color>" + "</b>" + " " + damageString + " " + "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
            else
                text.text = "<b>" + killer.transform.name + "</b>" + " Killed " + "<b>" + victim.transform.name + "</b>";
        }

        if (teamNumV == 2 && teamNumK == 1)
        {
            if (damageString != null)
                text.text = "<b>" + "<Color=#849dd6>" + killer.transform.name + "</Color>" + "</b>" + " " + damageString + " " + "<b>" + "<Color=#c3268a>" + victim.transform.name + "</Color>" + "</b>";
            else
                text.text = "<b>" + killer.transform.name + "</b>" + " Killed " + "<b>" + victim.transform.name + "</b>";
        }

        if (teamNumV == 1 && teamNumK == 2)
        {
            if (damageString != null)
                text.text = "<b>" + "<Color=#c3268a>" + killer.transform.name + "</Color>" + "</b>" + " " + damageString + " " + "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
            else
                text.text = "<b>" + killer.transform.name + "</b>" + " Killed " + "<b>" + victim.transform.name + "</b>";
        }

        if (teamNumV == 2 && teamNumK == 2)
        {
            if (damageString != null)
                text.text = "<b>" + "<Color=#c3268a>" + killer.transform.name + "</Color>" + "</b>" + " " + damageString + " " + "<b>" + "<Color=#c3268a>" + victim.transform.name + "</Color>" + "</b>";
            else
                text.text = "<b>" + killer.transform.name + "</b>" + " Killed " + "<b>" + victim.transform.name + "</b>";
        }
    }
}
