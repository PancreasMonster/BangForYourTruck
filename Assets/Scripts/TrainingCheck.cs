using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCheck : MonoBehaviour
{
    TrainingManager tm;

    // Start is called before the first frame update
    void Start()
    {
        tm = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tm.trainingStage == 1)
        {
            //check and activate bool for RTPressed and LTPressed on the trainingmanager
        }

        if (tm.trainingStage == 3)
        {
            //check and activate bool for APressed, XPressed, BPressed, YPressed on the trainingmanager
        }
    }
}
