using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FindPlayerStats : MonoBehaviour
{
    public GameObject player;

    GameObject throwableStats;
    float playerHealth;
    float playerMaxHealth;

    Text throwablesText2;
    Text throwablesText;
    Text resourceText;
    Text blackResourceText;
    Text resourceIncomeText;
    public Image hpBarFill;
    public Transform stats;
    // Start is called before the first frame update
    void Start()
    {

        throwableStats = stats.Find("ThrowablesStats").gameObject;
        throwablesText = stats.Find("ThrowablesStats").transform.Find("ThrowablesText").GetComponent<Text>();
        throwablesText2 = stats.Find("ThrowablesStats").transform.Find("ThrowablesText2").GetComponent<Text>();
        resourceText = stats.Find("Resources").GetComponent<Text>();
        blackResourceText = stats.Find("Resources (Black)").GetComponent<Text>();
        //resourceIncomeText = transform.Find("Resources Income").GetComponent<Text>();
        //hpBarFill = GetComponentInChildren<PrototypeHexMapScript>().gameObject.GetComponent<Image>();
        playerMaxHealth = player.GetComponent<Health>().maxHealth;
    }

    /*private void OnLeftBumper(InputValue value)
    {
        throwableStats.SetActive(true);
    }


    private void OnLeftBumperRelease(InputValue value)
    {
        throwableStats.SetActive(false);
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButtonDown("PadLB" + player.GetComponent<Health>().playerNum))
        {
            
        }
        else if (Input.GetButtonUp("PadLB" + player.GetComponent<Health>().playerNum))
        {

        }
        playerHealth = player.GetComponent<Health>().health;

        string throwableName = player.GetComponent<BuildModeFire>().currentDisc.transform.name.ToString();
        throwablesText.text = throwableName;
        throwablesText2.text = throwableName;
        resourceText.text = " $ " + ((int)player.GetComponent<PlayerBank>().tagsInBank).ToString();
        blackResourceText.text = " = " + ((int)player.GetComponent<PlayerBank>().tagsInBank).ToString();
        //resourceIncomeText = player.GetComponent<Resources(New)>().resourceIncomeAmount.ToString();
        hpBarFill.fillAmount = playerHealth / 100;

        if (playerHealth <= 0)
        {
         //   Show Killed By Text
        }
    }
}
