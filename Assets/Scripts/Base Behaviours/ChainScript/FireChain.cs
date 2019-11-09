using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireChain : MonoBehaviour
{
    public GameObject bolt, sprite, particleSys;
    GameObject lrPoint, psPoint;
    public Transform firePoint;
    public float force, bulletForce, cooldown = 1, fireRate, targetSpeed;
    bool coolingDown = false, fired, chainAttached, death = false;
    public List<GameObject> chains = new List<GameObject>();
    ParticleSystem ps;
    ConfigurableJoint dj;
    public Texture aTexture;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        AimAndFire();
        ChainRelease(death);
        /*if (psPoint != null)
        {
            Vector3 psDirection = new Vector3(lrPoint.transform.position.x, lrPoint.transform.position.y, 0) - new Vector3(transform.position.x, transform.position.y, 0);
            psDirection.z = 0;
            psDirection.Normalize();
            psPoint.transform.position = transform.TransformPoint(psDirection * (Vector2.Distance(transform.position, lrPoint.transform.position)));
            var mainShape = ps.shape;
            mainShape.radius = Vector2.Distance(firePoint.position, lrPoint.transform.position) / 2;
            float targetAng = Mathf.Atan2(psPoint.transform.position.y - lrPoint.transform.position.y, psPoint.transform.position.x - lrPoint.transform.position.x) * 180 / Mathf.PI + 90;

            psPoint.transform.rotation = Quaternion.Euler(-targetAng, 90, 90);
        } */

    }

    void AimAndFire()
    {



        if (Input.GetButtonDown("FireChain" + GetComponent<Health>().playerNum.ToString()) && !coolingDown && !chainAttached)
        {
           StartCoroutine(ShootHarpoon());
        }



    }

    IEnumerator ShootHarpoon()
    {
        coolingDown = true;
        GameObject clone = Instantiate(bolt, firePoint.position, Quaternion.identity);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        Chain chain = clone.GetComponent<Chain>();
        chain.BaseAssigner(this.gameObject);
        rb.AddForce(transform.forward * force);
        yield return new WaitForSeconds(cooldown);
        coolingDown = false;

    }

    public void ChainRelease(bool death1)
    {

        if ((Input.GetMouseButtonDown(1) && chainAttached) || death1 == true)
        {
            foreach (GameObject GO in chains)
            {
                Destroy(GO);
            }
            chains.Clear();
            chainAttached = false;
            Destroy(GetComponent<HingeJoint2D>());
            Destroy(dj);
            Destroy(ps);
            Destroy(lrPoint);
            Destroy(psPoint);
        }
    }




    public void ChainAttached(List<GameObject> chainSegments, GameObject LRPoint)
       {
         chainAttached = true;
          lrPoint = LRPoint;
        foreach (GameObject GO in chainSegments)
           {
              chains.Add(GO);
           }
       }

    public void HingeAndDistance(ConfigurableJoint DJ)
    {
        // dj = DJ;
        // GameObject clone = Instantiate(particleSys, transform.position, Quaternion.identity);
        //  psPoint = clone;
        //   clone.transform.rotation = Quaternion.Euler(90, 0, 0);
        //clone.rotation
        // ps = clone.GetComponent<ParticleSystem>();
        // var main = ps.main;
        // main.startSize = .25f;

    }

}
