using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeed : MonoBehaviour
{
    [SerializeField]
    GameObject killfeedItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void KillAnnouncement(GameObject killer, GameObject victim, string damageString)
    {
        GameObject ka = Instantiate(killfeedItemPrefab, this.transform);
        ka.GetComponent<KillFeedItem>().Setup(killer, victim, damageString);

        Destroy(ka, 12);
    }

    
}
