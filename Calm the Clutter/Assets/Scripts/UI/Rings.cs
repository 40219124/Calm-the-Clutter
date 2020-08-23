using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Rings : MonoBehaviour
{
    [SerializeField]
    Image outerRing;
    [SerializeField]
    Image innerRing;
    [SerializeField]
    Text statText;
    public float ringFillFraction = 1;

    public void SetColour(Color innerColour, Color outerColour)
    {
        outerRing.color = outerColour;
        innerRing.color = innerColour;
    }

    public void SetText(string text)
    {
        statText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        innerRing.fillAmount = ringFillFraction;
    }
}
