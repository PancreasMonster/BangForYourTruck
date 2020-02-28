using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralTagSpawner : MonoBehaviour
{
    public GameObject spawnTag;
    public Transform spawnPoint;
    public bool collected = true, cooldown = false;
    public float spawnTime = 20;

    // Start is called before the first frame update
    void Start()
    {
        GameObject neutralTag = Instantiate(spawnTag, spawnPoint.position, spawnTag.transform.rotation);
        neutralTag.GetComponent<NeutralTagPickUp>().spawner = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!collected && !cooldown)
        {
            StartCoroutine(SpawnTag());
        }
    }

    IEnumerator SpawnTag ()
    {
        collected = true;
        cooldown = true;
        yield return new WaitForSeconds(spawnTime);
        GameObject neutralTag = Instantiate(spawnTag, spawnPoint.position, spawnTag.transform.rotation);
        neutralTag.GetComponent<NeutralTagPickUp>().spawner = this.gameObject;
        cooldown = false;
    }
}
