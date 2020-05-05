using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTagSpawner : MonoBehaviour
{
    public GameObject killTag;
    public GameObject targetDrone;

    

    public void SpawnKillTag()
    {
        Instantiate(killTag,targetDrone.transform.position,targetDrone.transform.rotation);
    }
}
