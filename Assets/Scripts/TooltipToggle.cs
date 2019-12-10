using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipToggle : MonoBehaviour
{
    public Image tooltip;
    bool active = false;

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Back" + GetComponent<Health>().playerNum.ToString())) {
            if (active)
            {
                tooltip.gameObject.SetActive(false);
                active = false;
            }
            else {
                tooltip.gameObject.SetActive(true);
                active = true;
            }
        }
    }
}
