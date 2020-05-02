using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFeed : MonoBehaviour
{
    public GameObject scoreFeedItem;

    public void SetScoreFeedItem()
    {
        GameObject SFI = Instantiate(scoreFeedItem, this.transform);
        SFI.GetComponent<ScoreFeedItem>().setText();

        Destroy(SFI, 2);
    }
}
