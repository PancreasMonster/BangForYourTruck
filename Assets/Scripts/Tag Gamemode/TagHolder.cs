using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagHolder : MonoBehaviour
{
    public int currentTags;
    public GameObject teamTag;
    public float dropForce = 2000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropTags()
    {
        for(int i = 0; i < currentTags + 1; i++)
        {
            float angle = i * Mathf.PI * 2f / 8;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * 6, 0, Mathf.Sin(angle) * 6);
            GameObject droppedTag = Instantiate(teamTag, new Vector3(transform.position.x + newPos.x, transform.position.y + 1, transform.position.z + newPos.z), Quaternion.identity);
            droppedTag.GetComponent<TeamTagPickUp>().tagTeamNum = GetComponent<Health>().teamNum;
            droppedTag.GetComponent<Rigidbody>().AddForce(Vector3.up * dropForce);
        } 
    }
}
