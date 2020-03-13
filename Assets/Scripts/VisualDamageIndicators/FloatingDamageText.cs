using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    public float riseSpeed; //How fast the text rises after being instantiated 
    public TextMesh textMesh; //Text Mesh on this prefab
    public float origScaleAmount = 5; // The denominator of the original scale
    Vector3 origScale;
    float randomXSpeed, randomZSpeed;
    Color randomColor;


    // Start is called before the first frame update
    void Start()
    {
        origScale = transform.localScale; //Get the local scale on instantiation 
        randomXSpeed = Random.Range(0, 15);
        randomZSpeed = Random.Range(0, 15);

        randomColor = new Color(
        Random.Range(0f, 1f),
        Random.Range(0f, 1f),
        Random.Range(0f, 1f),
        1
        );
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.color = randomColor;
        transform.position += new Vector3(randomXSpeed * Time.deltaTime, riseSpeed * Time.deltaTime, randomZSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, origScale / origScaleAmount, 2 * Time.deltaTime);
        randomColor.a = Mathf.Lerp(1, 0, 4 * Time.deltaTime);
    }
}
