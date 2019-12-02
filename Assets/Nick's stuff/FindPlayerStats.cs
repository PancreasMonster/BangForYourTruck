using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindPlayerStats : MonoBehaviour
{
    public GameObject player;

    float playerHealth;
    float playerMaxHealth;

    Text resourceText;
    Text resourceIncomeText;
    Image hpBarFill;
    // Start is called before the first frame update
    void Start()
    {



        resourceText = transform.Find("Resources").GetComponent<Text>();
        resourceIncomeText = transform.Find("Resources Income").GetComponent<Text>();
        hpBarFill = GetComponentInChildren<PrototypeHexMapScript>().gameObject.GetComponent<Image>();
        playerMaxHealth = player.GetComponent<Health>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<Health>().health;

        resourceText.text = " = " + player.GetComponent<ResourceHolder>().resourceAmount.ToString();
        //resourceIncomeText = player.GetComponent<Resources(New)>().resourceIncomeAmount.ToString();
        hpBarFill.fillAmount = playerHealth / 100;

        if (playerHealth <= 0)
        {
         //   Destroy(this.gameObject);
        }
    }
}
