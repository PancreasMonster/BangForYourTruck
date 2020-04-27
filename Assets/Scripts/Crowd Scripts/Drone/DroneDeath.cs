using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDeath : MonoBehaviour
{
    public List<Transform> propellerPoints = new List<Transform>();
    Rigidbody rb;
    public float propellerForce;
    public float propellerRemovalTime;
    public GameObject droneDeathPS;
    float gravityForce;
    bool collidedWithGround = false;
    public float explosiveRadius, explosiveForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DroneDeathEffect());
        gravityForce = -25f;
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
        int rand = Random.Range(0, propellerPoints.Count-1);
        propellerPoints[rand].transform.parent = null;
        propellerPoints[rand].GetComponent<BoxCollider>().isTrigger = false;
        propellerPoints[rand].GetComponent<Rigidbody>().isKinematic = false;
        Vector3 randCircle = Random.insideUnitSphere;
        propellerPoints[rand].GetComponent<Rigidbody>().AddForce(randCircle * 100, ForceMode.Impulse);
        propellerPoints.Remove(propellerPoints[rand]);
        yield return new WaitForSeconds(propellerRemovalTime);
        if(propellerPoints.Count > 0)
        StartCoroutine(DroneDeathEffect());
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.layer == 14 && !collidedWithGround)
        {
            GetComponentInChildren<DroneBodySpin>().enabled = false;
            StopAllCoroutines();
            Instantiate(droneDeathPS, transform.position, Quaternion.identity);
            Debug.Log(coll.transform.name);
            foreach (Transform t in propellerPoints)
            {
                t.transform.parent = null;
                t.GetComponent<BoxCollider>().isTrigger = false;
                t.GetComponent<Rigidbody>().isKinematic = false;
                Vector3 randCircle = Random.insideUnitSphere;
                t.GetComponent<Rigidbody>().AddForce(randCircle * 100, ForceMode.Impulse);
            }
            propellerPoints.Clear();
            gravityForce = 300;
            rb.mass = 20;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRadius);
            foreach (Collider c in hitColliders)
            {
                Rigidbody rb = c.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(explosiveForce * rb.mass, transform.position, explosiveRadius);

                
            }
            collidedWithGround = true;
            Destroy(this.gameObject);
        }
        
    }
}
