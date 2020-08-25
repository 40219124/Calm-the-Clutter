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

    [SerializeField]
    Text announcerText;

    public static CatStats catStats;

    private int activeCoroutines = 0;
    private bool recentlyEndedTurn = false;

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
        StartCoroutine(WaitForNewTurn());
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
        if(activeCoroutines > 0 || HandManager.Instance.RunningAnimations() || recentlyEndedTurn)
        {
            return;
        }

        catStats.ChangeStat(EResource.sleep, 1);
        if(catStats.GetStat(EResource.sleep) >= 10)
        {
            StartCoroutine(EncounterVictory());
            return;
        }

        catStats.ChangeStat(EResource.danger, 2);
        catStats.ChangeStat(EResource.dirty, 2);
        catStats.ChangeStat(EResource.hunger, 2);

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

        StartCoroutine(WaitForNewTurn());
    }

    private IEnumerator EncounterVictory()
    {
        // ~~~ calc score
        int score = 30 - catStats.GetStat(EResource.danger) - catStats.GetStat(EResource.hunger) - catStats.GetStat(EResource.dirty);
        announcerText.text = $"You win!\nScore: {score}";
        // ~~~ reset deck
        HandManager.Instance.DiscardHand();
        DeckManager.Instance.EndEncounter();
        yield return new WaitForSeconds(3.0f);
        // ~~~ roll new cat
        FindObjectOfType<CatGenerator>().GenerateCat();
        catStats.Reset();
        announcerText.text = "";
        DeckManager.Instance.NewEncounter();
        StartCoroutine(WaitForNewTurn());
    }

    IEnumerator WaitForNewTurn()
    {
        recentlyEndedTurn = true;
        yield return new WaitForSeconds(1.0f);
        StartNewTurn();
        yield return new WaitForSeconds(1.0f);
        recentlyEndedTurn = false;
    }
    public void StartNewTurn()
    {

        HandManager.Instance.DrawNewHand();
        ManaGer.Instance.StartTurn();

    }

    bool showingImage;
    IEnumerator ShowImage(Image image)
    {
        activeCoroutines++;
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
        activeCoroutines--;
    }
}
