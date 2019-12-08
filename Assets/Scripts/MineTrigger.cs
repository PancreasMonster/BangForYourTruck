using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    public GameObject bomb;
    public int teamNum;
    public float primeTime = 1.5f;
    bool primed = false, triggered = false;
    public float lifeTime = 15f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(setPrime());
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (primed)
        {

            if (other.gameObject.GetComponent<Health>() != null && other.gameObject.GetComponent<Health>().playerNum != teamNum && !triggered)
            {
                Debug.Log("HIT Player");
                triggered = true;
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

