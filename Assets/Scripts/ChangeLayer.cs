using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    public string layerName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LayerDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LayerDelay ()
    {
        yield return new WaitForSeconds(2f);
        gameObject.layer = LayerMask.NameToLayer(layerName);
        ChangeLayersRecursively(transform, layerName);
    }

    private void ChangeLayersRecursively(Transform trans, string name)
    {
        foreach (Transform child in trans)
        {
            if (child.name != "Collider")
            {
                child.gameObject.layer = LayerMask.NameToLayer(name);
                ChangeLayersRecursively(child, name);
            }
        }
    }
}
