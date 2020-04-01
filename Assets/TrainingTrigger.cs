using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingTrigger : MonoBehaviour
{
    TrainingText trainingManager;

    public int trainingStage;

    // Start is called before the first frame update
    void Start()
    {
        trainingManager = GameObject.Find("Training Canvas").GetComponent<TrainingText>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (trainingManager.trainingStage == trainingStage)
        {
            trainingManager.GoToNextTraining();
            gameObject.SetActive(false);
        }       
    }
}
