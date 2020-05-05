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
            if (transform.name == "TrainingTrigger (Stage2)")
                manager.trigger1 = true;

            if (transform.name == "TrainingTrigger (Stage5)")
                if (player.GetComponent<TagHolder>().currentTags != 0)
                manager.trigger2 = true;
        }   
            
    }
}
