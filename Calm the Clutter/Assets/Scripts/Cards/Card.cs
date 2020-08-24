using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    public BasicCardScriptableObject basicCardInfo;
    public Image cardArtImage;
    public Text cardTitleText;
    public Text cardBodyText;
    public Text cardManaText;

    private Vector3 regularScale;
    private float zoomScale = 1.3f;

    bool isRayTarget = false;

    
    // Start is called before the first frame update
    void Start()
    {
        cardTitleText.text = basicCardInfo.cardTitle;
        cardArtImage.sprite = basicCardInfo.cardArt;
        cardManaText.text = basicCardInfo.cardMana.ToString();
        cardBodyText.text = FormatBodyText(basicCardInfo.cardText);

        regularScale = GetComponentInChildren<Canvas>().transform.localScale;
    }

    protected string FormatBodyText(string inText)
    {
        string outText = "";


        char[] inChars = inText.ToCharArray();

        int lastSubSEnd = 0;

        for (int i = 0; i < inText.Length; ++i)
        {
            if (inChars[i] == '{')
            {
                outText += inText.Substring(lastSubSEnd, i - lastSubSEnd);

                string sample = "";
                while (inChars[++i] != '}')
                {
                    sample += inChars[i];
                }

                string[] splits = sample.Split(',');
                bool multiplyNegative = false;
                EResource action = EResource.none;
                for (int j = 0; j < splits.Length; ++j)
                {
                    switch (splits[j])
                    {
                        case "-":
                            multiplyNegative = true;
                            break;
                        default:
                            action = (EResource)System.Enum.Parse(typeof(EResource), splits[j].ToLower());
                            break;
                    }
                }

                int value = 0;

                foreach (BasicEffect be in basicCardInfo.effects)
                {
                    if (be.resource == action)
                    {
                        value = be.effect;
                        break;
                    }
                }

                if (multiplyNegative)
                {
                    value *= -1;
                }

                outText += value;

                lastSubSEnd = i + 1;
            }
        }
        outText += inText.Substring(lastSubSEnd, inText.Length - lastSubSEnd);

        return outText;
    }

    public void CardClicked()
    {
        PerformAction();
    }

    protected virtual void PerformAction() {
        foreach(BasicEffect be in basicCardInfo.effects)
        {
            GameManager.catStats.ChangeStat(be.resource, be.effect);
        }
    }

    public void SetAsRaycastTarget(bool isTarget, int cards)
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        // ~~~ could be cleaner, not as much in the ifs
        if (isTarget)
        {
            canvas.sortingOrder = cards;
            canvas.transform.localScale = regularScale * zoomScale;
        }
        else
        {
            canvas.sortingOrder = cards;
            canvas.transform.localScale = regularScale;
        }

    }
}
