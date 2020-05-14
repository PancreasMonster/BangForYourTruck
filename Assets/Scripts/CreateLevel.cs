using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    int i = -1;
    public RotateAroundLevel ral;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("PersistentSceneLoader"))
        i = GameObject.Find("PersistentSceneLoader").GetComponent<LevelCreator>().levelToActivate;
        if(i != -1)
        activateLevel(i);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activateLevel (int levelIndex)
    {
        levels[levelIndex].SetActive(true);
        ral.level = levels[levelIndex].transform;
    }
}
