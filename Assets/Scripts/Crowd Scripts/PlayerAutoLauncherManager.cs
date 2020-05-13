using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAutoLauncherManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        players = GetComponent<KillManager>().players;
        StartCoroutine(BootPlayers());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator BootPlayers()
    {
        yield return new WaitForSeconds(startTime);
        foreach(GameObject p in players)
        {
            if(p.GetComponent<PlayerAutoLauncher>().launchPoint != null)
            p.GetComponent<PlayerAutoLauncher>().StartCountdown();
        }
    }
}
