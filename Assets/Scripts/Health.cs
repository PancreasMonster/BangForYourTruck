using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public float health, maxHealth;
    public int playerNum;
    public GameObject healthBarCanvas, hpBarHolder, hpBarHolder2, baseUI;
    Image hpBarFill;
    public bool mbase;

    // Start is called before the first frame update
    void Start()
    {
        if (!mbase)
        {
            maxHealth = health;

            GameObject hpBar = Instantiate(healthBarCanvas, new Vector3(transform.position.x, transform.position.y + 15f, transform.position.z), Quaternion.identity);
            hpBar.GetComponent<FaceCamera>().Cam1();
            GameObject hpBar2 = Instantiate(healthBarCanvas, new Vector3(transform.position.x, transform.position.y + 15f, transform.position.z), Quaternion.identity);
            hpBar2.GetComponent<FaceCamera>().Cam2();
            hpBar.GetComponent<FaceCamera>().mbase = transform;
            hpBar2.GetComponent<FaceCamera>().mbase = transform;
            hpBarHolder = hpBar;
            hpBarHolder2 = hpBar2;
            hpBarFill = hpBar.GetComponentInChildren<PrototypeHexMapScript>().gameObject.GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //hpBarFill.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            if (mbase)
                Destroy(baseUI);
            Destroy(hpBarHolder);
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
