using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindPlayerStats : MonoBehaviour
{
    public GameObject player;

    GameObject throwableStats;
    float playerHealth;
    float playerMaxHealth;

    Text throwablesText;
    Text resourceText;
    Text blackResourceText;
    Text resourceIncomeText;
    public Image hpBarFill;
    // Start is called before the first frame update
    void Start()
    {

        throwableStats = transform.Find("ThrowablesStats").gameObject;
        throwablesText = transform.Find("ThrowablesStats").transform.Find("ThrowablesText").GetComponent<Text>();
        resourceText = transform.Find("Resources").GetComponent<Text>();
        blackResourceText = transform.Find("Resources (Black)").GetComponent<Text>();
        //resourceIncomeText = transform.Find("Resources Income").GetComponent<Text>();
        //hpBarFill = GetComponentInChildren<PrototypeHexMapScript>().gameObject.GetComponent<Image>();
        playerMaxHealth = player.GetComponent<Health>().maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButtonDown("PadLB" + player.GetComponent<Health>().playerNum))
        {
            throwableStats.SetActive(true);
        }
        else if (Input.GetButtonUp("PadLB" + player.GetComponent<Health>().playerNum))
        {
            throwableStats.SetActive(false);
        }
        playerHealth = player.GetComponent<Health>().health;
        throwablesText.text = player.GetComponent<BuildModeFire>().currentDisc.transform.name.ToString();
        resourceText.text = " = " + ((int)player.GetComponent<PlayerBank>().tagsInBank).ToString();
        blackResourceText.text = " = " + ((int)player.GetComponent<PlayerBank>().tagsInBank).ToString();
        //resourceIncomeText = player.GetComponent<Resources(New)>().resourceIncomeAmount.ToString();
        hpBarFill.fillAmount = playerHealth / 100;

        if (playerHealth <= 0)
        {
         //   Show Killed By Text
        }
    }
}
