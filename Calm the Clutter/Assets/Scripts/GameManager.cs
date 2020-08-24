using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    CatGenerator catGen;
    [SerializeField]
    Rings Danger;
    [SerializeField]
    Rings Hunger;
    [SerializeField]
    Rings Dirty;
    [SerializeField]
    Rings Sleep;

    [SerializeField]
    Image attack;

    [SerializeField]
    Image dirt;

    [SerializeField]
    Image teeth;

    public static CatStats catStats;

    private void Awake()
    {
        catGen.GenerateCat();
        catStats = new CatStats();
        Danger.SetText("Danger");
        Hunger.SetText("Hunger");
        Dirty.SetText("Dirtiness");
        Sleep.SetText("Sleepiness");
    }

    private void Start()
    {
        DeckManager.Instance.NewEncounter();
        HandManager.Instance.DrawNewHand();
    }

    // Update is called once per frame
    void Update()
    {
        Danger.ringFillFraction = (float)(catStats.GetStat(EResource.danger)) / (float)CatStats.max;
        Hunger.ringFillFraction = (float)(catStats.GetStat(EResource.hunger)) / (float)CatStats.max;
        Dirty.ringFillFraction = (float)(catStats.GetStat(EResource.dirty)) / (float)CatStats.max;
        Sleep.ringFillFraction = (float)(catStats.GetStat(EResource.sleep)) / (float)CatStats.max;
    }

    public void OnEndTurn()
    {
        catStats.ChangeStat(EResource.danger, 2);
        catStats.ChangeStat(EResource.dirty, 2);
        catStats.ChangeStat(EResource.hunger, 2);
        catStats.ChangeStat(EResource.sleep, 1);

        if (catStats.GetStat(EResource.danger) >= 10)
        {
            StartCoroutine(ShowImage(attack));
        }
        if (catStats.GetStat(EResource.hunger) >= 10)
        {
            StartCoroutine(ShowImage(teeth));
        }
        if (catStats.GetStat(EResource.dirty) >= 10)
        {
            StartCoroutine(ShowImage(dirt));
        }

        HandManager.Instance.DiscardHand();
        ManaGer.Instance.EndTurn();

        HandManager.Instance.DrawNewHand();
        ManaGer.Instance.StartTurn();
    }

    bool showingImage;
    IEnumerator ShowImage(Image image)
    {
        while (showingImage)
        {
            yield return null;
        }

        showingImage = true;
        float timePassed = 0;

        Color colour = image.color;
        while (colour.a < 1)
        {
            colour.a += 1 * Time.deltaTime;
            image.color = colour;
            yield return null;
        }

        colour.a = 0;
        image.color = colour;
        showingImage = false;
    }
}
