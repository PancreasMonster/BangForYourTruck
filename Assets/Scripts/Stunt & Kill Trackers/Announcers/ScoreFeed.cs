using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFeed : MonoBehaviour
{
    public GameObject scoreFeedItem;

    public void SetScoreFeedItem(string scoreText, List<string> flavour)
    {
        GameObject SFI = Instantiate(scoreFeedItem, this.transform);
        SFI.GetComponent<ScoreFeedItem>().setText(scoreText, flavour);

        
    }
}
