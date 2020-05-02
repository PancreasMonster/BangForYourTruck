using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFeedItem : MonoBehaviour
{
    public Text text;


    public void setText()
    {
        text.text = "+100 Kill";
    }
}
