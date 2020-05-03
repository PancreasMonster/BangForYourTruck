using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingTrigger : MonoBehaviour
{
    TrainingManager manager;

    public int neededStagetoProceed;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (manager.trainingStage == neededStagetoProceed)
        {
            manager.ProceedTraining();
        }       
    }
}
