using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingTrigger : MonoBehaviour
{
    TrainingManager manager;
    public GameObject player;

    public int neededStagetoProceed;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && manager.trainingStage == neededStagetoProceed)
        {
            if (manager.trainingStage == 2)
            {
                manager.trainingStage = 3;
                manager.MoveToArena();
            }
            if (manager.trainingStage == 7 && other.gameObject.GetComponent<TagHolder>().currentTags > 0)
            {

                manager.trainingStage = 8;
                manager.TagDeposit();
            }
        }   
            
    }
}
