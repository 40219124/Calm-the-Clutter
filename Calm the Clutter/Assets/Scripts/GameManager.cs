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

    public CatStats catStats;

    private void Awake()
    {
        catGen.GenerateCat();
        catStats = new CatStats();
    }

    // Update is called once per frame
    void Update()
    {
        Danger.ringFillPercent = catStats.GetStat(EResource.danger);
        Hunger.ringFillPercent = catStats.GetStat(EResource.hunger);
        Dirty.ringFillPercent = catStats.GetStat(EResource.dirty);
        Sleep.ringFillPercent = catStats.GetStat(EResource.sleep);
    }
}
