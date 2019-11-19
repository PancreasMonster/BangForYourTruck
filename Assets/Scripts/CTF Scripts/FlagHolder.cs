using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagHolder : MonoBehaviour
{

    public bool flagTaken;

    public GameObject flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flagTaken)
            flag.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    }

    public void TakeFlag (GameObject enemyFlag)
    {
        flagTaken = true;
        flag = enemyFlag;
    }

    public void DropFlag()
    {
        if (flag != null)
        {
            flagTaken = false;          
            flag.GetComponent<Flag>().StartTimer();
            flag = null;
        }
    }

    public void DeliverFlag()
    {
        if (flag != null)
        {
            flagTaken = false;          
        }
    }

    public void FallOff()
    {
        if (flag != null)
        {
            flagTaken = false;
            flag.GetComponent<Flag>().FallOff();
        }
    }
}
