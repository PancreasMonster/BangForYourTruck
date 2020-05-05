using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardScene : MonoBehaviour
{
    public GameObject podium;
    public Transform podiumPlacement;
    public List<GameObject> players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpPodium ()
    {
        GameObject pod = Instantiate(podium, podiumPlacement.transform);
        pod.transform.localPosition = Vector3.zero;
    }
}
