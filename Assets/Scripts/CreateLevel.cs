using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public List<Transform> players = new List<Transform>();
    public List<GameObject> levels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activateLevel (int levelIndex)
    {
        levels[levelIndex].SetActive(true);
        for(int i = 0; i < players.Count; i++)
        {
            players[i].position = levels[levelIndex].transform.Find("PlayerSpawns").transform.Find("Player" + i.ToString() + "SpawnPoint").position;
        }
    }
}
