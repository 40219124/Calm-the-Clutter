using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        Danger.ringFillFraction = (float)(catStats.GetStat(EResource.danger)) / (float)CatStats.max;
        Hunger.ringFillFraction = (float)(catStats.GetStat(EResource.hunger)) / (float)CatStats.max;
        Dirty.ringFillFraction = (float)(catStats.GetStat(EResource.dirty)) / (float)CatStats.max;
        Sleep.ringFillFraction = (float)(catStats.GetStat(EResource.sleep)) / (float)CatStats.max;
    }
}
