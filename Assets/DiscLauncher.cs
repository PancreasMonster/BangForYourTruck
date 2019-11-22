using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscLauncher : MonoBehaviour
{
    public float discSpeed;
    public float rateOfFire;
    public GameObject sawdisc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p")) {
            StartCoroutine(FireDiscs());
        }

        if (Input.GetKeyUp("p"))
            StopAllCoroutines();
    }

    IEnumerator FireDiscs()
    {
        yield return new WaitForSeconds(rateOfFire);
        LaunchDisc();
        StartCoroutine(FireDiscs());
    }

    void LaunchDisc() {
        GameObject disc = Instantiate(sawdisc, transform.position, transform.rotation);
        disc.GetComponent<Rigidbody>().AddForce(0, 0, discSpeed, ForceMode.Impulse);
    }
}
