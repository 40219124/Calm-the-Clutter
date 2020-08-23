using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECardToTransform
{
    public ECard card;
    public Transform transform;
}
[CreateAssetMenu(fileName = "CardDictionary", menuName = "ScriptableObjects/CardDictionary")]
public class CardDictionary : ScriptableObject
{
    public List<ECardToTransform> cardPrefabs = new List<ECardToTransform>();
}
