using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFeed : MonoBehaviour
{
    public GameObject scoreFeedItem;
    List<GameObject> listOfScoreFeedItems = new List<GameObject>();
    float timer;
    int amountOfSFIs;

    public void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }


    public void SetScoreFeedItem(List<string> flavour, List<int> scoreList)
    {
        
        if (amountOfSFIs == 0)
        {
            StartCoroutine(scoreFeedStarter(0, flavour, scoreList));
            amountOfSFIs++;
        }
        else
        {
            float timeForScoreFeedItem;
            if(flavour.Count < 2)
            {
                timeForScoreFeedItem = ((2.25f) * amountOfSFIs);
                Debug.Log(timeForScoreFeedItem);
                StartCoroutine(scoreFeedStarter(timeForScoreFeedItem, flavour, scoreList));
                amountOfSFIs++;
            } else
            {
                timeForScoreFeedItem = ((1.25f + ((flavour.Count - 1) * .75f)) * amountOfSFIs);
                Debug.Log(timeForScoreFeedItem);
                StartCoroutine(scoreFeedStarter(timeForScoreFeedItem, flavour, scoreList));
                amountOfSFIs++;
            }
        }
        
    }

    IEnumerator scoreFeedStarter(float time, List<string> flavour, List<int> scoreList)
    {
        
        if (time == 0)
        timer = time;
        yield return new WaitForSeconds(time);
        GameObject SFI = Instantiate(scoreFeedItem, this.transform);       
        SFI.GetComponent<ScoreFeedItem>().setText(flavour, scoreList);
        if(time == 0)
            yield return new WaitForSeconds(2.25f);
        amountOfSFIs--;

    }
}
