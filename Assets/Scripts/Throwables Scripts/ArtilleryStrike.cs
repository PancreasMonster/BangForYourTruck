using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryStrike : MonoBehaviour
{
    Vector3 position;
    public float yOffset;
    public int spawnAmount;
    public float timeBetweenSpawns;

    Rigidbody rb;
    ArtillerySpawner spawner;
    public int amountSpawned = 0;

    public GameObject artillerySpawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        GameObject spawned = Instantiate(artillerySpawner, position, transform.rotation);
        spawner = spawned.GetComponent<ArtillerySpawner>();
        Spawn();
        transform.GetChild(0).gameObject.SetActive(false);
        //Destroy(this.gameObject,2f);

        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ;
    }

    void Spawn()
    {
        spawner.SpawnArtilleryShell();
        amountSpawned++;

        if (amountSpawned < spawnAmount)
        {
            Invoke("Spawn", timeBetweenSpawns);
        }
    }

}

    