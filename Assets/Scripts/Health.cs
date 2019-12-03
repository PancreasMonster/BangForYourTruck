using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public float health, maxHealth, currentHealth;
    public int playerNum;
    public GameObject healthBarCanvas, hpBarHolder, hpBarHolder2, baseUI;
    Image hpBarFill, hpBarFill2;
    public bool mbase;
    bool dead;
    public PlayerRespawn pr;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        currentHealth = health;
        if (!mbase)
        {
           

            GameObject hpBar = Instantiate(healthBarCanvas, new Vector3(transform.position.x, transform.position.y + 3.25f, transform.position.z), Quaternion.identity);
            hpBar.GetComponent<FaceCamera>().Cam1();
            GameObject hpBar2 = Instantiate(healthBarCanvas, new Vector3(transform.position.x, transform.position.y + 3.25f, transform.position.z), Quaternion.identity);
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
        if (!mbase)
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

            if (currentHealth > health)
            {
                GetComponent<FlagHolder>().DropFlag();
                currentHealth = health;
            }

            if (currentHealth < health)
            {
                currentHealth = health;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //hpBarFill.fillAmount = health / maxHealth;
        if (health <= 0 && !dead)
        {
            dead = true;
            if (mbase)
            {
                //Destroy(baseUI);
                StartCoroutine(deadTime());
            }

            if (!mbase)
            {
                Destroy(this.gameObject);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                if (GetComponentInChildren<ExplodeOnDeath>() != null)
                {
                    BroadcastMessage("Explode");


                    if (GetComponentInChildren<Turret>() != null)
                    {
                        Turret turret = GetComponentInChildren<Turret>();
                        turret.enabled = false;

                    }
                }

                if (GetComponentInChildren<BaseExplodeOnDeath>() != null)
                {
                    BroadcastMessage("Explode");

                }
            }
        }
    }

    public IEnumerator deadTime ()
    {
        pr.playerDeath(playerNum);
        //hpBarHolder.SetActive(false);
        yield return new WaitForSeconds(pr.deathTimer);
        dead = false;
        
        //hpBarHolder.SetActive(true);
    }
}
