using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFeed : MonoBehaviour
{
    public GameObject scoreFeedItem;

    public void SetScoreFeedItem(List<string> flavour, List<int> scoreList)
    {
        GameObject SFI = Instantiate(scoreFeedItem, this.transform);
        SFI.GetComponent<ScoreFeedItem>().setText(flavour, scoreList);

        
    }
}
