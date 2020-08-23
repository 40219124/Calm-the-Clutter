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
    [SerializeField]
    SpriteRenderer Eyes;

    [SerializeField]
    List<SpriteRenderer> Markings;
    [SerializeField]
    List<Sprite> mouths;
    [SerializeField]
    List<Sprite> eyes;

    [SerializeField]
    Animator Animator;

    string TailFloofAnimParam = "TailFluff";

    public void GenerateCat()
    {
        int catBod = Random.Range(0, CatColours.Count);
        int catTail = Random.Range(0, CatColours.Count);
        int catNose = Random.Range(0, CatNoseColours.Count);

        Body.color = CatColours[catBod];
        Tail.color = CatColours[catTail];
        Face.color = CatNoseColours[catNose];

        for (int i = 0; i < Markings.Count; ++i)
        {
            int catMark = Random.Range(0, CatColours.Count);
            Markings[i].color = CatColours[catMark];
        }

        int tailType = Random.Range(0, 2);
        Animator.SetInteger(TailFloofAnimParam, tailType);

        float size = Random.Range(0.5f, 2.1f);
        transform.localScale = new Vector3(size, size);
        Debug.Log($"GenerateCat! catBod: {catBod}, catNose: {catNose}, catTail: {catTail}, size = {size}");
    }

    private void Update()
    {
        int mouth = Mathf.Clamp(GameManager.catStats.CalculateAnger(), 0, mouths.Count - 1);
        Face.sprite = mouths[mouth];

        int sleep = Mathf.Clamp(GameManager.catStats.GetStat(EResource.sleep), 0, eyes.Count - 1);
        Eyes.sprite = eyes[sleep];
    }

}
