using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreBoard : MonoBehaviour
{
    public TagCollectionManager tcm;
    public List<GameObject> scoreBoardItems = new List<GameObject>();

    private void OnStart(InputValue value)
    {
        if(!tcm.gameWon)
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
