using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    ConfigurableJoint cj;
    public GameObject pbase; // parent base
    public GameObject chain, lrPoint;
    GameObject enemySpawn;
    Vector3 hitPoint;
    List<GameObject> chains = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject, .5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != pbase)
        {
            hitPoint = collision.contacts[0].point;
            GameObject col = collision.transform.gameObject;
            GameObject clone = Instantiate(lrPoint, hitPoint, Quaternion.identity);
            clone.transform.parent = col.transform;
            createRope(col);
            Destroy(this.gameObject);
            pbase.GetComponent<FireChain>().ChainAttached(chains, clone);
        }
    }

    public void BaseAssigner(GameObject parent)
    {
        pbase = parent;
    }

    void createRope(GameObject package)
    {
        Vector3 direction = pbase.transform.position - hitPoint;
        direction.z = 0;
        direction.Normalize();
        float distance = Vector3.Distance(pbase.transform.position, hitPoint);  
        ConfigurableJoint cj = pbase.AddComponent<ConfigurableJoint>();
        cj.connectedBody = package.GetComponent<Rigidbody>();
        cj.autoConfigureConnectedAnchor = false;
        cj.anchor = new Vector3(0, .5f, 0);//package.transform.InverseTransformPoint(hitPoint);
        SoftJointLimit sjl = new SoftJointLimit();
        sjl.limit = 10;
        sjl.bounciness = 1;
        cj.linearLimit = sjl;
        JointDrive jd = new JointDrive();
        jd.positionSpring = 100;
        jd.positionDamper = 10;
        cj.xDrive = jd;
        cj.yDrive = jd;
        cj.zDrive = jd;
        cj.xMotion = ConfigurableJointMotion.Locked;
        cj.yMotion = ConfigurableJointMotion.Locked;
        cj.zMotion = ConfigurableJointMotion.Locked;
        pbase.GetComponent<FireChain>().HingeAndDistance(cj);
       
    }
}

