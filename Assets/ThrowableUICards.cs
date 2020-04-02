using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableUICards : MonoBehaviour
{
    public Transform[] childTransforms;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(GameObject cardToAdd)
    {
        foreach(Transform child in childTransforms)
        {
            if (child.childCount == 0)
            {
                GameObject addedCard =Instantiate(cardToAdd, child.position, child.rotation);
                // .Add(addedCard.GetComponent<ThrowableCooldown>().cooldownTime); this to the list of discCooldown(s) in the players buildmodefire
                return;
            }
        }

        
    }
}
