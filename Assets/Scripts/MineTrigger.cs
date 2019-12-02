using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    public GameObject bomb;
    public float primeTime = 1.5f;
    bool primed = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(setPrime()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (primed)
        {

            if (other.gameObject.tag == "Player")
            {
                Debug.Log("HIT Player");

                Trigger();
            }
        }
    }

       void Trigger() {
        Instantiate(bomb, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), transform.rotation);
            GetComponentInParent<ExplosiveMine>().DestroyThisGameObject();
    }

    IEnumerator setPrime ()
    {
        yield return new WaitForSeconds(primeTime);
        primed = true;
    }
}

