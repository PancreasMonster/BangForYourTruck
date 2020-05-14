using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    int i = -1;
    public RotateAroundLevel ral;
    GameObject PSL;
    public PlayerPause pp;
    public TagCollectionManager tcm;
    public bool trainingMode = false;

    // Start is called before the first frame update
    void Start()
    {
        PSL = GameObject.Find("PersistentSceneLoader");
        if (trainingMode && PSL)
        {
            Destroy(PSL);
            return;
        }

        if (GameObject.Find("PersistentSceneLoader"))
            i = PSL.GetComponent<LevelCreator>().levelToActivate;
            
        if (i != -1)
            activateLevel(i);


        Destroy(PSL);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FindValue()
    {
        yield return new WaitForSeconds(.25f);
        
    }

    public void activateLevel (int levelIndex)
    {
        levels[levelIndex].SetActive(true);
        ral.level = levels[levelIndex].transform;
    }
}
