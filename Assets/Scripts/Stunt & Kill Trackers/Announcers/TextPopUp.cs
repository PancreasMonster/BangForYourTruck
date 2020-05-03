using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    public ScoreFeed scoreFeed;

    public void ScoreFeedMessage(string scoreText, List<string> flavour)
    {
        scoreFeed.SetScoreFeedItem(scoreText, flavour);
    }
}
