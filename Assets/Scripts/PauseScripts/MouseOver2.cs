using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOver2 : MonoBehaviour, IPointerExitHandler
{


    Image image;
    Button b;

    public void OnPointerExit(PointerEventData eventData)
    {

        image.color = b.colors.normalColor;
    }



    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        b = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnMouseEnter()
    {

    }
}

