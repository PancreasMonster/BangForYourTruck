using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool noUI;
    public PlayerPause pp;
    public int i;
    Image image;
    Button b;

    public void OnPointerEnter(PointerEventData eventData)
    {
        noUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        noUI = false;
        image.color = b.colors.normalColor;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        b = GetComponent<Button>();
    }

    public void OnEnable()
    {
        noUI = false;
    }

    // Update is called once per frame
    void Update()
    {       
        if(noUI)
        {
            pp.ChangeToMouse(i);
            Debug.Log("Yup");
        }

    }

    public void OnMouseEnter()
    {
        
    }
}
