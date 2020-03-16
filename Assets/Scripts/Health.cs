using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public float health, maxHealth, currentHealth;
    public int playerNum;
    public int teamNum;
    public GameObject healthBarCanvas, hpBarHolder, hpBarHolder2, baseUI, car, wheel;
    Image hpBarFill, hpBarFill2;
    public bool mbase;
    bool dead;
    public PlayerRespawn pr;
    public GameObject damageText;
    public int damageTextLayer;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        currentHealth = health;
        if (!mbase)
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!mbase && !dead)
        {
            hpBarFill.fillAmount = health / maxHealth;
            hpBarFill2.fillAmount = health / maxHealth;
        }
        if (mbase)
        {
            if (health > maxHealth)
            {
                health = maxHealth;
            }         
        }

        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //hpBarFill.fillAmount = health / maxHealth;

        if (health <= 0 && !dead)
            Death();

    }

    public void TakeDamage(string playerSourceString, GameObject playerSourceGameObject, float damageTaken, Vector3 damagePoint)
    {
        if (dead)
            return;

        //Instantiates the damage text mesh on the players position
        GameObject damageTextGameObject = Instantiate(damageText, transform.position, Quaternion.identity);

        //The text is equal to the damage taken
        damageTextGameObject.GetComponentInChildren<TextMesh>().text = ((int)damageTaken).ToString();

        //Changes the layer of the text so only the opposite team can see it
        damageTextGameObject.gameObject.layer = damageTextLayer;
        foreach (Transform t in damageTextGameObject.transform)
        {
            t.gameObject.layer = damageTextLayer;
        }

        //Makes the text mesh face the player 
        damageTextGameObject.transform.LookAt(playerSourceGameObject.transform);

        //Deals the damage to the player's health
        health -= damageTaken;

        if (health - damageTaken <= 0)
            Death();
    }

    public void Death()
    {
        health = 0;
        dead = true;
        if (mbase)
        {
            //Destroy(baseUI);
            StartCoroutine(deadTime());
        }

        if (!mbase)
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
        GameObject Car = Instantiate(car, transform.position, transform.rotation);
        Rigidbody carRB = Car.GetComponent<Rigidbody>();
        carRB.AddForce((Vector3.up * 80000) + GetComponent<Rigidbody>().velocity);
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
        dead = false;

        //hpBarHolder.SetActive(true);
    }

    
}