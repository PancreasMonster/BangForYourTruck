using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorUI : MonoBehaviour
{
    public Image image;
    public GameObject player;
    public Transform target;
    public Camera cam;
    public Vector3 offset;
    float rot = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.rectTransform.position = cam.WorldToScreenPoint(new Vector3(player.transform.position.x, player.transform.position.y+1, player.transform.position.z)) + offset;
       // rot += Time.deltaTime;
       // offset = Quaternion.AngleAxis(90 * Time.deltaTime, Vector3.forward) * offset;
        Vector3 dir = cam.WorldToScreenPoint(target.position) - image.rectTransform.position;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        image.rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // dir.Normalize();
        //  Debug.Log(dir);
        //image.rectTransform.RotateAround(image.rectTransform.position, Vector3.forward, 90);
    }
}
