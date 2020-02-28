using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    int i;
    // Start is called before the first frame update
    void Start()
    {
        i = GameObject.Find("PersistentSceneLoader").GetComponent<LevelCreator>().levelToActivate;
        activateLevel(i);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activateLevel (int levelIndex)
    {
        levels[levelIndex].SetActive(true);       
    }
}
