using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseGate : MonoBehaviour
{
    public int price;
    public GameObject throwableType;
    public GameObject throwableCard;
    public float cooldown;
    public int teamGateNum;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        TextMesh text = transform.parent.GetComponentInChildren<TextMesh>();
        text.text = text.text + "Cost: " + price.ToString();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "Player")
        {
            if (col.GetComponent<Health>().teamNum == teamGateNum)
            {
                PlayerBank PB = col.GetComponent<PlayerBank>();
                if (PB.tagsInBank >= price)
                {
                    if (!col.GetComponent<BuildModeFire>().discSelection.Contains(throwableType))
                    {
                        col.GetComponent<BuildModeFire>().discSelection.Add(throwableType);
                        col.GetComponent<BuildModeFire>().discCooldown.Add(cooldown);
                        GameObject.Find("PlayerStatsUICanvas").transform.GetChild(col.GetComponent<Health>().playerNum - 1)
                            .transform.Find("Throwables Cards").GetComponent<ThrowableUICards>().AddCard(throwableCard);
                        PB.tagsInBank -= price;
                        audio.Play();
                    }
                    
                }
            }
        }
    }
}