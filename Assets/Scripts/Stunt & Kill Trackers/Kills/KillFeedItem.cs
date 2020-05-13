using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillFeedItem : MonoBehaviour
{
    [SerializeField]
    public Image killImage;
    public Sprite nullImage;
    public List<Text> texts = new List<Text>();

    public void Setup (GameObject killer, GameObject victim, Sprite sourceImage, int teamNumV, int teamNumK)
    {
        if(killer == victim)
        {
            if (teamNumK == 1)
            {
                if (sourceImage != null)
                {
                    texts[0].text = "<b>" + "<Color=#849dd6>" + "        " + "</Color>" + "</b>";
                    killImage.sprite = sourceImage;
                    texts[1].text = "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
                }
                else
                {
                    texts[0].text = "<b>" + "<Color=#849dd6>" + "        " + "</Color>" + "</b>";
                    killImage.sprite = nullImage;
                    texts[1].text = "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
                }
            } else
            {
                if (sourceImage != null)
                {
                    texts[0].text = "<b>" + "<Color=#c3268a>" + "        " + "</Color>" + "</b>";
                    killImage.sprite = sourceImage;
                    texts[1].text = "<b>" + "<Color=#c3268a>" + victim.transform.name + "</Color>" + "</b>";
                }
                else
                {
                    texts[0].text = "<b>" + "<Color=#c3268a>" + "        " + "</Color>" + "</b>";
                    killImage.sprite = nullImage;
                    texts[1].text = "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
                }
            }
        }
        else if (teamNumV == 1 && teamNumK == 1)
        {
            if (sourceImage != null)
            {
                texts[0].text = "<b>" + "<Color=#849dd6>" + killer.transform.name + "</Color>" + "</b>";
                killImage.sprite = sourceImage;
                texts[1].text = "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
            }
            else
            {
                texts[0].text = "<b>" + "<Color=#849dd6>" + killer.transform.name + "</Color>" + "</b>";
                killImage.sprite = nullImage;
                texts[1].text = "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
            }              
        }
        else if (teamNumV == 2 && teamNumK == 1)
        {
            if (sourceImage != null)
            {
                texts[0].text = "<b>" + "<Color=#849dd6>" + killer.transform.name + "</Color>" + "</b>";
                killImage.sprite = sourceImage;
                texts[1].text = "<b>" + "<Color=#c3268a>" + victim.transform.name + "</Color>" + "</b>";
            }
            else
            {
                texts[0].text = "<b>" + "<Color=#849dd6>" + killer.transform.name + "</Color>" + "</b>";
                killImage.sprite = nullImage;
                texts[1].text = "<b>" + "<Color=#c3268a>" + victim.transform.name + "</Color>" + "</b>";
            }
        }
        else if (teamNumV == 1 && teamNumK == 2)
        {
            if (sourceImage != null)
            {
                texts[0].text = "<b>" + "<Color=#c3268a>" + killer.transform.name + "</Color>" + "</b>";
                killImage.sprite = sourceImage;
                texts[1].text = "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
            }
            else
            {
                texts[0].text = "<b>" + "<Color=#c3268a>" + killer.transform.name + "</Color>" + "</b>";
                killImage.sprite = nullImage;
                texts[1].text = "<b>" + "<Color=#849dd6>" + victim.transform.name + "</Color>" + "</b>";
            }
        }
        else if (teamNumV == 2 && teamNumK == 2)
        {
            if (sourceImage != null)
            {
                texts[0].text = "<b>" + "<Color=#c3268a>" + killer.transform.name + "</Color>" + "</b>";
                killImage.sprite = sourceImage;
                texts[1].text = "<b>" + "<Color=#c3268a>" + victim.transform.name + "</Color>" + "</b>";
            }
            else
            {
                texts[0].text = "<b>" + "<Color=#c3268a>" + killer.transform.name + "</Color>" + "</b>";
                killImage.sprite = nullImage;
                texts[1].text = "<b>" + "<Color=#c3268a>" + victim.transform.name + "</Color>" + "</b>";
            }
        }
    }
}
