using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    public ScoreFeed scoreFeed;

    public void ScoreFeedMessage()
    {
        scoreFeed.SetScoreFeedItem();
    }
}
