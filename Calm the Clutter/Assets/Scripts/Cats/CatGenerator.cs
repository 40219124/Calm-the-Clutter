using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatGenerator : MonoBehaviour
{
    [SerializeField]
    List<Color> CatColours;
    [SerializeField]
    List<Color> CatNoseColours;

    [SerializeField]
    SpriteRenderer Body;
    [SerializeField]
    SpriteRenderer Tail;
    [SerializeField]
    SpriteRenderer Face;

    void Awake()
    {
        GenerateCat();
    }

    void GenerateCat()
    {
        int catBod = Random.Range(0, CatColours.Count);
        int catNose = Random.Range(0, CatNoseColours.Count);
        Debug.Log($"catBod: {catBod}, catNose: {catNose}");

        Body.color = CatColours[catBod];
        Tail.color = CatColours[catBod];
        Face.color = CatNoseColours[catNose];
    }

}
