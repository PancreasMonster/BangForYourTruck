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
    GameObject levelInfoParent;

    // Start is called before the first frame update
    void Start()
    {
        levelInfoParent = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadImage()
    {
        transform.GetChild(1).GetComponent<Image>().sprite = spriteToDisplay;

        if (spriteToDisplay == gridGulch)
        {
            levelInfoParent.transform.GetChild(0).gameObject.SetActive(true);
            levelInfoParent.transform.GetChild(1).gameObject.SetActive(false);
            levelInfoParent.transform.GetChild(2).gameObject.SetActive(false);
        } 
        
        else if (spriteToDisplay == caverns)
        {
            levelInfoParent.transform.GetChild(0).gameObject.SetActive(false);
            levelInfoParent.transform.GetChild(1).gameObject.SetActive(true);
            levelInfoParent.transform.GetChild(2).gameObject.SetActive(false);
        }

        else if (spriteToDisplay == valleys)
        {
            levelInfoParent.transform.GetChild(0).gameObject.SetActive(false);
            levelInfoParent.transform.GetChild(1).gameObject.SetActive(false);
            levelInfoParent.transform.GetChild(2).gameObject.SetActive(true);
        }
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

    public void TurnOffLevelInfo()
    {
        levelInfoParent.SetActive(false);

    }
}
