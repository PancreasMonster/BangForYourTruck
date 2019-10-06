using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1);
    }

    public Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * 10 * Time.deltaTime);
    }
}
