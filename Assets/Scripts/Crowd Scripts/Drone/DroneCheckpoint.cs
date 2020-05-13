using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCheckpoint : MonoBehaviour
{
    bool triggered = false;
    public TrainingManager tm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Drone" && !triggered)
        {
            
            if(tm)
            {
                tm.trainingStage = 1;
                triggered = true;
            } else
            {
                other.GetComponent<TrainingDrone>().AdvanceToNextWaypoint();
                triggered = true;
            }
        }


    }
}
