﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagHolder : MonoBehaviour
{
    public int currentTags;
    public GameObject teamTag;
    public float dropForce = 2000;
    GameObject tagHolder;
    ParticleSystem particles;
    KillManager km;



    // Start is called before the first frame update
    void Start()
    {
        tagHolder = transform.Find("KillTag Holder").gameObject;
        particles = transform.Find("Item Pickup Particle").GetComponent<ParticleSystem>();
        km = GetComponent<KillManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTag()
    {
        currentTags++;
        if (currentTags == 1) {
            tagHolder.transform.GetChild(0).gameObject.SetActive(true);
            tagHolder.GetComponent<AudioSource>().Play();
            particles.Play();
        }

        if (currentTags == 2) {
            tagHolder.transform.GetChild(1).gameObject.SetActive(true);
            tagHolder.GetComponent<AudioSource>().Play();
            particles.Play();
        }

        if (currentTags == 3) {
            tagHolder.transform.GetChild(2).gameObject.SetActive(true);
            tagHolder.GetComponent<AudioSource>().Play();
            particles.Play();
        }
    }

    public void dropTags()
    {
        for(int i = 0; i < currentTags + 1; i++)
        {
            float angle = i * Mathf.PI * 2f / 8;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * 6, 0, Mathf.Sin(angle) * 6);
            GameObject droppedTag = Instantiate(teamTag, new Vector3(transform.position.x + newPos.x, transform.position.y + 1, transform.position.z + newPos.z), Quaternion.identity);
            droppedTag.GetComponentInChildren<TeamTagPickUp>().tagTeamNum = GetComponent<Health>().teamNum;
            droppedTag.GetComponentInChildren<TeamTagPickUp>().km = km;
            droppedTag.GetComponent<Rigidbody>().AddForce(Vector3.up * dropForce);            
            currentTags = 0;
        }
        EmptyTags();
    }

    public void EmptyTags ()
    {
        tagHolder.transform.GetChild(0).gameObject.SetActive(false);
        tagHolder.transform.GetChild(1).gameObject.SetActive(false);
        tagHolder.transform.GetChild(2).gameObject.SetActive(false);
    }
}
