using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTagSpawner : MonoBehaviour
{
    public GameObject killTag;
    public GameObject originPoint;

    

    public void SpawnKillTag()
    {
        Instantiate(killTag, originPoint.transform.position, originPoint.transform.rotation);
    }
}
