using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDeath : MonoBehaviour
{
    public List<Transform> propellerPoints = new List<Transform>();
    Rigidbody rb;
    public float propellerForce;
    public float propellerRemovalTime;
    float gravityForce;
    bool collidedWithGround = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DroneDeathEffect());
        gravityForce = 37.5f;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform t in propellerPoints)
        {
            rb.AddForceAtPosition(t.transform.up * propellerForce, t.position);
        }
        rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
    }

    IEnumerator DroneDeathEffect ()
    {
        int rand = Random.Range(0, propellerPoints.Count);
        propellerPoints[rand].transform.parent = null;
        propellerPoints[rand].GetComponent<BoxCollider>().isTrigger = false;
        propellerPoints[rand].GetComponent<Rigidbody>().isKinematic = false;
        propellerPoints[rand].GetComponent<Rigidbody>().AddForce(transform.up * propellerForce * 5);
        propellerPoints.Remove(propellerPoints[rand]);
        yield return new WaitForSeconds(propellerRemovalTime);
        if(propellerPoints.Count > 0)
        StartCoroutine(DroneDeathEffect());
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.layer == 14 && !collidedWithGround)
        {
            StopAllCoroutines();
            //Explosion Here
            Debug.Log(coll.transform.name);
            foreach (Transform t in propellerPoints)
            {
                t.transform.parent = null;
                t.GetComponent<BoxCollider>().isTrigger = false;
                t.GetComponent<Rigidbody>().isKinematic = false;
                t.GetComponent<Rigidbody>().AddForce(transform.up * propellerForce * 5f);
            }
            propellerPoints.Clear();
            gravityForce = 100;
            rb.mass = 20;
            //Explosion Damage Here
            collidedWithGround = true;
        }
        
    }
}
