using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float health, maxHealth;
    public int playerNum;
    public GameObject healthBarCanvas, hpBarHolder, baseUI;
    Image hpBarFill;
    public bool mbase;

    // Start is called before the first frame update
    void Start()
    {
        GameObject hpBar = Instantiate(healthBarCanvas, new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), Quaternion.identity);
        hpBar.GetComponent<FaceCamera>().mbase = transform;
        hpBarHolder = hpBar;
        hpBarFill = hpBar.GetComponentInChildren<PrototypeHexMapScript>().gameObject.GetComponent<Image>();
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        hpBarFill.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            if (mbase)
                Destroy(baseUI);
            Destroy(hpBarHolder);
            Destroy(this.gameObject);
        }
    }
}
