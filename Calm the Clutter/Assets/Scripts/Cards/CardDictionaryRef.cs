using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDictionaryRef : MonoBehaviour
{
    #region singleton
    private static CardDictionaryRef instance;
    public static CardDictionaryRef Instance
    {
        get { return instance; }
    }
    #endregion

    [SerializeField]
    private CardDictionary cardDictionaryObject;

    public Dictionary<ECard, Transform> cards = new Dictionary<ECard, Transform>();

    private void Awake()
    {
        instance = this;
        foreach (ECardToTransform ct in cardDictionaryObject.cardPrefabs)
        {
            if (!cards.ContainsKey(ct.card))
            {
                cards.Add(ct.card, ct.transform);
            }
        }
    }

}
