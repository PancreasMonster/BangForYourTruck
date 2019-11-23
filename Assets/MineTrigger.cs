using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT Player");

            Trigger();
        }
    }

       void Trigger() {
            Instantiate(bomb, transform.position, transform.rotation);
            GetComponentInParent<ExplosiveMine>().DestroyThisGameObject();
    }
}

