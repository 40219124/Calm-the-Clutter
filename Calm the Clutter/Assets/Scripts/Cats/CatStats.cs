using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStats
{
    int danger = 0;
    int sleep = 0;
    int hunger = 0;
    int dirty = 0;
    public static readonly int max = 10;

    public int GetStat(EResource stat)
    {
        switch (stat)
        {
            case EResource.danger:
                return danger;
            case EResource.sleep:
                return sleep;
            case EResource.hunger:
                return hunger;
            case EResource.dirty:
                return dirty;
            default:
                return 0;
        }
    }

    public void ChangeStat(EResource stat, int value)
    {
        switch (stat)
        {
            case EResource.danger:
                danger += value;
                danger = Mathf.Clamp(danger, 0, max);
                break;
            case EResource.sleep:
                sleep += value;
                sleep = Mathf.Clamp(sleep, 0, max);
                break;
            case EResource.hunger:
                hunger += value;
                hunger = Mathf.Clamp(hunger, 0, max);
                break;
            case EResource.dirty:
                dirty += value;
                dirty = Mathf.Clamp(dirty, 0, max);
                break;
            default:
                break;
        }
    }

}
