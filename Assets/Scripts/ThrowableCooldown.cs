using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ThrowableCooldown : MonoBehaviour
{
    public float cooldownTime;
    float t = 0f;
    bool onCooldown;
    public float fillAmountValue = 1;
    public Image[] images = new Image[2];
    public Animation selectionAnim;

    // Start is called before the first frame update
    void Start()
    {
        images[0] = transform.GetChild(2).GetComponent<Image>();
        images[1] = transform.GetChild(3).GetComponent<Image>();
        selectionAnim = transform.Find("SelectingText").GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
       
        foreach (Image i in images)
        {
            i.fillAmount = fillAmountValue;
        }

        if (onCooldown)
        {
            //Debug.Log("LERPING BACK TO COLOR");
            fillAmountValue += Time.deltaTime / cooldownTime;

            if (fillAmountValue >= 1)
            {
                Debug.Log("FINISHED");
                onCooldown = false;
                selectionAnim.Play();
            }
        }
    }

    public void GoOnCooldown(float time)
    {

        //Debug.Log("GOING ON COOLDOWN");
        fillAmountValue = time;
        onCooldown = true;
    }
}
