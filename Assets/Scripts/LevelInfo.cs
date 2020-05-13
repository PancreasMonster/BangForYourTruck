using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelInfo : MonoBehaviour
{
    Sprite spriteToDisplay;

    public Sprite gridGulch;
    public Sprite valleys;
    public Sprite caverns;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadImage()
    {
        tranform.GetChild(1).GetComponent<Image>().sprite = spriteToDisplay;
    }

    public void GridGulchImage()
    {
        spriteToDisplay = gridGulch;
    }

    public void ValleysImage()
    {
        spriteToDisplay = valleys;

    }

    public void CavernsImage()
    {
        spriteToDisplay = caverns;

    }

    
}
