using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public List<int> kills = new List<int>();
    public List<int> deaths = new List<int>();

    public TextAnnouncement TA;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillTracked (GameObject killer, GameObject victim)
    {
        deaths[victim.GetComponent<Health>().playerNum - 1]++;
        kills[killer.GetComponent<Health>().playerNum - 1]++;
        TA.PlayerKill(killer, victim);
    }

}
