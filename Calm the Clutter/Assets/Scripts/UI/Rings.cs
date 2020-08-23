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
    public float ringFillPercent = 100;

    public void SetColour(Color innerColour, Color outerColour)
    {
        outerRing.color = outerColour;
        innerRing.color = innerColour;
    }

    // Update is called once per frame
    void Update()
    {
        innerRing.fillAmount = ringFillPercent / 100;
    }
}
