using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtillerySpawner : MonoBehaviour
{
    public GameObject artillery;
    public float randomXZPositionOffest;
    public float randomYPositionOffset;
    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


    public void SpawnArtilleryShell()
    {
        float randomXOffset = Random.Range(-randomXZPositionOffest/2, randomXZPositionOffest/2);
        float randomYOffset = Random.Range(-randomYPositionOffset/2, randomYPositionOffset/2);
        float randomZOffset = Random.Range(-randomXZPositionOffest/2, randomXZPositionOffest/2);
        Vector3 spawnPos = new Vector3(transform.position.x + randomXOffset, transform.position.y + randomYOffset, transform.position.z + randomZOffset);
        Instantiate(artillery, spawnPos, transform.rotation);

        Debug.Log(randomXOffset.ToString() + randomYOffset.ToString() + randomZOffset.ToString());
    }
}
