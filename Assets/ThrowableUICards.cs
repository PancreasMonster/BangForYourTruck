using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableUICards : MonoBehaviour
{
    public Transform[] childTransforms = new Transform[5];
    public GameObject card;

    // Start is called before the first frame update
    void Start()
    {

        childTransforms = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && card != null)
        {
            AddCard(card);
            Debug.Log("Fafaf");
        }
    }

    public void AddCard(GameObject cardToAdd)
    {
        foreach(Transform child in childTransforms)
        {
            if (child.childCount == 0)
            {
                GameObject addedCard = Instantiate(cardToAdd, child.position, child.rotation);
                addedCard.transform.parent = child;
                // .Add(addedCard.GetComponent<ThrowableCooldown>().cooldownTime); this to the list of discCooldown(s) in the players buildmodefire
                return;
            }
        }

        
    }
}
