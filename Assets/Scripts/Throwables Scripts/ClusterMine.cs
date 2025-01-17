﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterMine : MonoBehaviour
{
    public GameObject mine;
    public float force;
    public GameObject source;
   

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
                firedMine.GetComponent<MineTrigger>().source = source;
            }
            if (i == 1)
            {
                firedMine.GetComponent<Rigidbody>().AddForce(((Vector3.forward + Vector3.right) + Vector3.up) * (force * .75f), ForceMode.Impulse);
                firedMine.GetComponent<MineTrigger>().source = source;
            }
            if (i == 2)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.left + Vector3.up) * force, ForceMode.Impulse);
                firedMine.GetComponent<MineTrigger>().source = source;
            }
            if (i == 3)
            {
                firedMine.GetComponent<Rigidbody>().AddForce(((Vector3.back + Vector3.right) + Vector3.up) * (force * .75f), ForceMode.Impulse);
                firedMine.GetComponent<MineTrigger>().source = source;
            }
            if (i == 4)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.back + Vector3.up) * force, ForceMode.Impulse);
                firedMine.GetComponent<MineTrigger>().source = source;
            }
            if (i == 5)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.back + Vector3.left + Vector3.up) * (force * .75f), ForceMode.Impulse);
                firedMine.GetComponent<MineTrigger>().source = source;
            }
            if (i == 6)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.right + Vector3.up) * force, ForceMode.Impulse);
                firedMine.GetComponent<MineTrigger>().source = source;
            }
            if (i == 7)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.forward + Vector3.left + Vector3.up) * (force * .75f), ForceMode.Impulse);
                firedMine.GetComponent<MineTrigger>().source = source;
            }
            if (i == 8)
            {
                firedMine.GetComponent<Rigidbody>().AddForce((Vector3.up) * force * 1.5f, ForceMode.Impulse);
                firedMine.GetComponent<MineTrigger>().source = source;
            }
        }
        Destroy(this.gameObject, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        SpawnMines();
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
