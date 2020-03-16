using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindPlayerStats : MonoBehaviour
{
    public GameObject player;

    float playerHealth;
    float playerMaxHealth;

    Text throwablesText;
    Text resourceText;
    Text resourceIncomeText;
    public Image hpBarFill;
    // Start is called before the first frame update
    void Start()
    {


        throwablesText = transform.Find("ThrowablesStats").transform.Find("ThrowablesText").GetComponent<Text>();
        resourceText = transform.Find("Resources").GetComponent<Text>();
        //resourceIncomeText = transform.Find("Resources Income").GetComponent<Text>();
        //hpBarFill = GetComponentInChildren<PrototypeHexMapScript>().gameObject.GetComponent<Image>();
        playerMaxHealth = player.GetComponent<Health>().maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerHealth = player.GetComponent<Health>().health;
        //throwablesText.text = player.GetComponent<BuildModeFire>().currentDisc.transform.name.ToString();
       // resourceText.text = " = " + ((int)player.GetComponent<PlayerBank>().tagsInBank).ToString();
        //resourceIncomeText = player.GetComponent<Resources(New)>().resourceIncomeAmount.ToString();
        hpBarFill.fillAmount = playerHealth / 100;

        if (playerHealth <= 0)
        {
         //   Show Killed By Text
        }
    }
}
