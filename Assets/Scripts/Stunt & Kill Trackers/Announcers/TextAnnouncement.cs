using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnnouncement : MonoBehaviour
{
    [SerializeField]
    GameObject killfeedItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
      /*  announcementTextColor = announcementText.color;
        imageColor = image.color;
        announcementTextColor.a = 0;
        imageColor.a = 0; */
    }

    public void KillAnnouncement (GameObject killer, GameObject victim, Sprite sourceImage, int teamNumV, int teamNumK)
    {
        GameObject ka = Instantiate(killfeedItemPrefab, this.transform);
        ka.GetComponent<KillFeedItem>().Setup(killer, victim, sourceImage, teamNumV, teamNumK);
    }

    /*
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
            announcementText.text = "<b>" + killer.transform.name + "</b>" + " " + damageString + " " + "<b>" + victim.transform.name + "</b>";
        else
            announcementText.text = "<b>" + killer.transform.name + "</b>" + " " + "Has Killed + " + "<b>" + victim.transform.name + "</b>";
        announcementTextColor.a = 1;
        imageColor.a = 1;
        delayTimer = 5;
    } */
}
