using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralPurchaseGate : MonoBehaviour
{
    public int price;
    public GameObject throwableType;
    public GameObject throwableCard;
    public float cooldown;
    AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {

            PlayerBank PB = col.GetComponent<PlayerBank>();
            if (PB.tagsInBank >= price)
            {
                if (!col.GetComponent<BuildModeFire>().discSelection.Contains(throwableType))
                {
                    col.GetComponent<BuildModeFire>().discSelection.Add(throwableType);
                    // GameObject.Find("PlayerStatsUICanvas").transform.GetChild(col.GetComponent<Health>().playerNum - 1)
                    //    .transform.Find("Throwables Cards").GetComponent<ThrowableUICards>().AddCard(throwableCard);
                    GameObject addCard = Instantiate(throwableCard, transform.position, Quaternion.identity);
                    addCard.transform.parent = col.GetComponent<BuildModeFire>().cardParent;
                    addCard.transform.localScale = new Vector3(1, 1, 1);
                    col.GetComponent<BuildModeFire>().discUIImages.Add(addCard);
                    addCard.GetComponent<ThrowableCooldown>().cooldownTime = cooldown;
                    PB.tagsInBank -= price;
                    audio.Play();
                }

            }
           
        }
    }
}
