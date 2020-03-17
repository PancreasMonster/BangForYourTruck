using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuntScoreText : MonoBehaviour
{
    Text text;
    public StuntChecker sc;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Stunt Score : " + sc.score.ToString("n0");
    }
}
