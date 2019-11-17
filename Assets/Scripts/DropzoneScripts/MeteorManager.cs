﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorManager : MonoBehaviour
{
    public GameObject meteor;
    public List<GameObject> dropZones = new List<GameObject>();
    public Text text;
    public float delayBetweenMeteorStrikes = 60;
    public float laserGap = 15;
    public float meteorSpawnHeight = 100;
    public float meteorSpawnCircleRadius = 10;

    private float timer;
    public ParticleSystem ps;
    public LineRenderer lr;
    public float lrWidth = 0, psLife = 0;
    float lrWidthMax = 50;
    private bool start, stop, startLaser, stopLaser;
    private float origWidth;
   // public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MeteorStrike());
        timer = delayBetweenMeteorStrikes;
        lr.startWidth = lrWidth;
        lr.endWidth = lrWidth;
        origWidth = lrWidth;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = timer.ToString();
        timer -= Time.deltaTime;
        SkyLaser();
    }

    void SkyLaser ()
    {
        lr.startWidth = lrWidth;
        lr.endWidth = lrWidth;

        var main = ps.main;
        main.startLifetime = psLife;
        if (startLaser && lrWidth < lrWidthMax && !stopLaser)
        {
            lrWidth += 1f * 100 * (lrWidthMax / 200) * Time.deltaTime;
        }

        if (stopLaser && lrWidth > 0)
        {
            lrWidth -= 1f * 100 * (lrWidthMax / 200) * Time.deltaTime;
        }

        if (start && psLife < .66f * 25f && !stop)
        {
            psLife += .16f * 25f * Time.deltaTime;
        }

        if (stop)
        {
            psLife = 0;
        }
    }

    IEnumerator MeteorStrike ()
    {
        yield return new WaitForSeconds (delayBetweenMeteorStrikes - laserGap);
        int rand = Random.Range(0, dropZones.Count-1);      
        Vector3 spawnHeight = new Vector3(dropZones[rand].transform.position.x, dropZones[rand].transform.position.y + meteorSpawnHeight, dropZones[rand].transform.position.z);
        Vector3 randCircle = Random.insideUnitCircle * meteorSpawnCircleRadius;
        Vector3 spawnVector = new Vector3(spawnHeight.x + randCircle.x, spawnHeight.y, spawnHeight.z + randCircle.y);       
        lrWidth = origWidth;
        lr.startWidth = lrWidth;
        lr.endWidth = lrWidth;
        lr.SetPosition(0, spawnHeight);
        lr.SetPosition(1, new Vector3(dropZones[rand].transform.position.x, dropZones[rand].transform.position.y - 5f, dropZones[rand].transform.position.z));
        StartCoroutine(StartLaser());
        yield return new WaitForSeconds(laserGap);
        timer = delayBetweenMeteorStrikes;
        
        GameObject clone = Instantiate(meteor, spawnVector, Quaternion.identity);
        clone.GetComponent<Meteor>().AssignTarget(dropZones[rand]);
        StartCoroutine(MeteorStrike());
    }

    public IEnumerator StartLaser()
    {
        //aud.Play();
        
        start = true;
        yield return new WaitForSeconds(4.5f);
        startLaser = true;
        yield return new WaitForSeconds(.2f);
        stop = true;
        stopLaser = true;
        yield return new WaitForSeconds(2);
        lrWidth = 0;        
        start = false;
        startLaser = false;
        stop = false;
        stopLaser = false;
    }
}