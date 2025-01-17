﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{

    public float health, maxHealth;
    public int playerNum;
    public int teamNum;
    public GameObject healthBarCanvas, hpBarHolder, hpBarHolder2, baseUI, car, wheel;
    Image hpBarFill, hpBarFill2;
    public bool mbase;
    bool dead;
    public PlayerRespawn pr;
    public GameObject damageText;
    public int damageTextLayer;
    public KillManager km;
    public float killerTimer;
    public float killMaxTime = 15;
    public GameObject damageSource;
    public bool drone, targetDrone;
    Sprite damageSourceImage;
    public TagCollectionManager tcm;
    PlayerPause pp;

    [FMODUnity.EventRef]
    public string dronePainSound;

    bool dronePainCooldown = true;

    void OnFaceButtonSouth(InputValue value)
    {
        if (tcm)
            tcm.AButton();
    }

    void OnFaceButtonEast(InputValue value)
    {
        if (tcm)
            tcm.BButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        pp = GetComponent<PlayerPause>();
        if (GameObject.Find("KillManager") == true)
        {
            km = GameObject.Find("KillManager").GetComponent<KillManager>();
        }

        maxHealth = health;
       /* if (!mbase)
        {


            GameObject hpBar = Instantiate(healthBarCanvas, new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z), Quaternion.identity);
            hpBar.GetComponent<FaceCamera>().Cam1();
            GameObject hpBar2 = Instantiate(healthBarCanvas, new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z), Quaternion.identity);
            hpBar2.GetComponent<FaceCamera>().Cam2();
            hpBar.GetComponent<FaceCamera>().mbase = transform;
            hpBar2.GetComponent<FaceCamera>().mbase = transform;
            hpBarHolder = hpBar;
            hpBarHolder2 = hpBar2;
            hpBarFill = hpBar.GetComponentInChildren<PrototypeHexMapScript>().gameObject.GetComponent<Image>();
            hpBarFill2 = hpBar2.GetComponentInChildren<PrototypeHexMapScript>().gameObject.GetComponent<Image>();
        } */
    }

    // Update is called once per frame
    void Update()
    {
        if(killerTimer > 0)
        {
            killerTimer -= Time.deltaTime;
        } else
        {
            damageSource = null;
            damageSourceImage = null;
        }


        if (mbase)
        {
            if (health > maxHealth)
            {
                health = maxHealth;
            }         
        }



        //hpBarFill.fillAmount = health / maxHealth;

        if (health <= 0 && !dead)
        
            Death();


    }

    public void TakeDamage(Sprite sourceImage, GameObject playerSourceGameObject, float damageTaken, Vector3 damagePoint)
    {
        //If dead, stop code here
        if (dead)
            return;

        killerTimer = killMaxTime;
        damageSource = playerSourceGameObject;
        damageSourceImage = sourceImage;


        //Instantiates the damage text mesh on the players position
        if (damageTaken >= 1)
        {
            GameObject damageTextGameObject = Instantiate(damageText, transform.position, Quaternion.identity);

            //The text is equal to the damage taken
            damageTextGameObject.GetComponentInChildren<TextMesh>().text = ((int)damageTaken).ToString();


            damageTextGameObject.GetComponentInChildren<TextMesh>().fontSize = ((int)Mathf.Min(550, Mathf.Max(150, 550 * damageTaken / maxHealth)));

            //Changes the layer of the text so only the opposite team can see it
            damageTextGameObject.gameObject.layer = damageTextLayer;
            foreach (Transform t in damageTextGameObject.transform)
            {
                t.gameObject.layer = damageTextLayer;
            }

            //Makes the text mesh face the player 
            damageTextGameObject.transform.LookAt(playerSourceGameObject.transform);
        }

        //Deals the damage to the player's health
        float damageToTake = Mathf.Max(damageTaken, 0);
        health -= damageToTake;

        if (drone && dronePainCooldown)
        {
            FMODUnity.RuntimeManager.PlayOneShot(dronePainSound);
            dronePainCooldown = false;
            StartCoroutine(DronePainCooldown());
        }

        
    }

    public void Death()
    {
       
        health = 0;
        dead = true;
        if (mbase)
        {
            if (GameObject.Find("KillManager") == true)
            {
                km.KillTracked(damageSource, this.gameObject, damageSourceImage, teamNum, damageSource.GetComponent<Health>().teamNum);
            }

            pp.noJumpOrBoost = true;
            //Destroy(baseUI);
            StartCoroutine(deadTime());
        }

        if(drone)
        {
            GetComponent<DroneScript>().DeathTrigger(damageSource.GetComponent<Health>().playerNum);
        }

        if (targetDrone)
        {
            //GetComponent<KillTagSpawner>().SpawnKillTag();
            GetComponent<TargetDrone>().DeathTrigger();
            GameObject.Find("AlfaHorizon").GetComponent<LockOn>().targets.Clear();
        }

        if (!mbase && !drone)
        {
            Destroy(this.gameObject, 5);
            Destroy(hpBarHolder);
            Destroy(hpBarHolder2);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (GetComponentInChildren<ExplodeOnDeath>() != null)
            {
                BroadcastMessage("Explode");



            }

            if (GetComponentInChildren<Turret>() != null)
            {
                GetComponentInChildren<Turret>().enabled = false;
                GetComponentInChildren<Turret>().StopAllCoroutines();

            }

            if (GetComponentInChildren<BaseExplodeOnDeath>() != null)
            {
                BroadcastMessage("Explode");

            }
        }
    }

    public IEnumerator deadTime()
    {
        pp.noPlayerInput = true;
        GameObject Car = Instantiate(car, transform.position, transform.rotation);
        Rigidbody carRB = Car.GetComponent<Rigidbody>();
        carRB.AddForce((Vector3.up * 80000) + GetComponent<Rigidbody>().velocity * 10);
        //Car.GetComponentInChildren<BaseExplodeOnDeath>().Explode();
        Destroy(Car, 10);
        for (int x = 0; x < 2; x++)
        {
            for (int z = 0; z < 2; z++)
            {
                GameObject Wheel = Instantiate(wheel, new Vector3(transform.position.x - 2.5f + (x * 5), transform.position.y + 1f, transform.position.z + .1f + (-1.1f * z)), Quaternion.identity);
                Rigidbody wheelRB = Wheel.GetComponent<Rigidbody>();
                wheelRB.AddForce(Random.onUnitSphere * 7500);
                Destroy(Wheel, 10);
            }
        }
        pr.playerDeath(playerNum, Car.transform);
        //hpBarHolder.SetActive(false);
        yield return new WaitForSeconds(pr.deathTimer);
        if(!tcm.scoreBoardShown)
        pp.noPlayerInput = false;
        dead = false;

        //hpBarHolder.SetActive(true);
    }

    IEnumerator DronePainCooldown ()
    {
        yield return new WaitForSeconds(8);
        dronePainCooldown = true;
    }


}