using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCooldown : MonoBehaviour
{
    public float cooldownTime;
    float t = 0f;
    Color spriteColor;
    bool onCooldown;
    public Color startingColor;

    // Start is called before the first frame update
    void Start()
    {
        spriteColor = transform.GetChild(1).GetComponent<SpriteRenderer>().material.color;
        startingColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, spriteColor.a);

        GoOnCooldown();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (onCooldown)
        {
            Debug.Log("LERPING BACK TO COLOR");
            spriteColor = Color.Lerp(spriteColor, startingColor, t);

            if (t >= 1)
            {
                Debug.Log("FINISHED");
                onCooldown = false;
            }
        }

        if (t < 1)
        {
            t += Time.deltaTime / cooldownTime;
        }
    }

    public void GoOnCooldown()
    {

        Debug.Log("GOING ON COOLDOWN");
        spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0f);
        t = 0f;
        onCooldown = true;
    }
}
