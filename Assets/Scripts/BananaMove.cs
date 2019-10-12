using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject target;
    public Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, dir, .1f);
        if (Vector3.Distance(transform.position, dir) < 0.1)
        {
            target.GetComponent<Health>().health -= 20;
            Destroy(this.gameObject);
        }

        if (target == null)
            Destroy(this.gameObject);
    }
}
