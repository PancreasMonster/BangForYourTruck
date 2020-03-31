using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnnouncement : MonoBehaviour
{
    public Text announcementText;
    public Image image;
    public float fadeDelay = 5;
    float delayTimer;
    Color announcementTextColor, imageColor;

    // Start is called before the first frame update
    void Start()
    {
        announcementTextColor = announcementText.color;
        imageColor = image.color;
        announcementTextColor.a = 0;
        imageColor.a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        announcementText.color = announcementTextColor;
        image.color = imageColor;

        if(delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }

        if(announcementTextColor.a > 0 && delayTimer <= 0)
        {
            announcementTextColor.a = Mathf.Lerp(announcementTextColor.a, 0, 1f * Time.deltaTime);
        }

        if (imageColor.a > 0 && delayTimer <= 0)
        {
            imageColor.a = Mathf.Lerp(imageColor.a, 0, 1f * Time.deltaTime);
        }
    }

    public void PlayerKill (GameObject killer, GameObject victim, string damageString)
    {
        if(damageString != null)
            announcementText.text = killer.transform.name + " " + damageString + " " + victim.transform.name;
        else
            announcementText.text = killer.transform.name + " has killed " + victim.transform.name;
        announcementTextColor.a = 1;
        imageColor.a = 1;
        delayTimer = 5;
    }
}
