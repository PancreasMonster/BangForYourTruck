using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterMine : MonoBehaviour
{
    public GameObject mine;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        //SpawnMines();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SpawnMines();
        }
    }

    void SpawnMines()
    {
        int childNum = transform.GetChild(0).childCount;

        for (int i = 0; i < childNum; i++)
        {
            Transform firingPoint = transform.GetChild(0).transform.GetChild(i);
            GameObject firedMine = Instantiate(mine, firingPoint.position, firingPoint.rotation);
            if (i == 0)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.forward + Vector3.up) * force, ForceMode.Impulse);
            }
            if (i == 1)
            {
                firedMine.GetComponent<Rigidbody>().AddForce(((Vector3.forward + Vector3.right) + Vector3.up) * (force * .75f), ForceMode.Impulse);
            }
            if (i == 2)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.left + Vector3.up) * force, ForceMode.Impulse);
            }
            if (i == 3)
            {
                firedMine.GetComponent<Rigidbody>().AddForce(((Vector3.back + Vector3.right) + Vector3.up) * (force * .75f), ForceMode.Impulse);
            }
            if (i == 4)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.back + Vector3.up) * force, ForceMode.Impulse);
            }
            if (i == 5)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.back + Vector3.left + Vector3.up) * (force * .75f), ForceMode.Impulse);
            }
            if (i == 6)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.right + Vector3.up) * force, ForceMode.Impulse);
            }
            if (i == 7)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.forward + Vector3.left + Vector3.up) * (force * .75f), ForceMode.Impulse);
            }
            if (i == 8)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.up) * force * 1.5f, ForceMode.Impulse);
            }
        }
        //transform.GetChild(0).DetachChildren();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        SpawnMines();
    }
}
