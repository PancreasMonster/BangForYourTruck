using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    public ScoreFeed scoreFeed;

    public void ScoreFeedMessage(List<string> flavour, List<int> scoreList)
    {
        scoreFeed.SetScoreFeedItem(flavour, scoreList);
    }
}
