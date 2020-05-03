using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFeedItem : MonoBehaviour
{
    public Text text;
    Text scoreAdderText;
    public List<Text> flavourTexts = new List<Text>();
    public List<Text> textsGameObjects = new List<Text>();
    public GameObject textObject;
    int currentText = 0;
    List<RectTransform> rectTrans = new List<RectTransform>();
    List<Vector2> tvectors = new List<Vector2>();
    List<int> scoreAmounts = new List<int>();
    Vector2 origPos;
    int score;
    int adderScore;
    int targetScore;

    public void Start()
    {
        origPos = new Vector2(text.rectTransform.localPosition.x + 35, text.rectTransform.localPosition.y);
    }

    public void Update()
    {
        for (int i = 0; i < textsGameObjects.Count; i++)
        {
            rectTrans[i].localPosition = Vector2.MoveTowards(rectTrans[i].localPosition, tvectors[i], 2);
            textsGameObjects[i].color = new Color(1, 1, 1, 25 - (((float)Vector2.Distance(origPos, rectTrans[i].localPosition)/1.5f)));
        }

        score = Mathf.FloorToInt(Mathf.MoveTowards(((float)score), ((float)targetScore), 4));
        text.text = "+" + score.ToString();
       // if(adderScore > 0)
      //  adderScore = Mathf.FloorToInt(Mathf.MoveTowards(((float)adderScore), 0, 4));
       // scoreAdderText.text = "+" + adderScore.ToString();
    }

    public void setText(List<string> flavour, List<int> scoreList)
    {
        score = scoreList[0];
        targetScore = scoreList[0];
        text.text = "+" + score.ToString();
        scoreAmounts = scoreList;
       // Text sAT = Instantiate(text, this.transform);
        //sAT.GetComponent<RectTransform>().localPosition = new Vector2(text.rectTransform.localPosition.x, text.rectTransform.localPosition.y - 15);
       // scoreAdderText = sAT;
        //scoreAdderText.color = Color.yellow;
        for (int i = 0; i < flavour.Count; i++)
        {
            Text g = Instantiate(text, this.transform);
            Text t = g.GetComponent<Text>();
            t.text = flavour[i];
            g.GetComponent<RectTransform>().localPosition = new Vector2(text.rectTransform.localPosition.x + 35, text.rectTransform.localPosition.y + (25 * i));
            
            textsGameObjects.Add(g);
            rectTrans.Add(textsGameObjects[i].GetComponent<RectTransform>());
            tvectors.Add(rectTrans[i].localPosition);
            Color c = textsGameObjects[i].color;
            g.color = new Color(c.r, c.g, c.b, 25 - (((float)Vector2.Distance(origPos, rectTrans[i].localPosition) / 1.5f)));
        }
        StartCoroutine(MovingText());
    }

    IEnumerator MovingText()
    {
        if (textsGameObjects.Count > 1)
        {
            if (currentText == 0)
                yield return new WaitForSeconds(.5f);

            for (int i = 0; i < textsGameObjects.Count; i++)
            {
                Vector2 v = rectTrans[i].localPosition;
                tvectors[i] = new Vector2(v.x, v.y - 25);
            }
            yield return new WaitForSeconds(.5f);
            targetScore = score + scoreAmounts[currentText];
          //  adderScore = scoreAmounts[currentText];
            yield return new WaitForSeconds(.25f);
           // scoreAdderText.text = "";
            currentText += 1;
            if (currentText < textsGameObjects.Count - 1)
            {
                StartCoroutine(MovingText());
            }
            else
            {
                yield return new WaitForSeconds(.5f);
                Destroy(this.gameObject);
            }
        } else
        {
            yield return new WaitForSeconds(2f);
            Destroy(this.gameObject);
        }
    }
}
