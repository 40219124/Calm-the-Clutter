using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    #region singleton
    private static DeckManager instance;
    public static DeckManager Instance
    {
        get { return instance; }
    }
    #endregion
    private void Awake()
    {
        instance = this;
        GiveStartingDeck();
    }


    public List<ECard> deck = new List<ECard>();

    public List<ECard> drawPile = new List<ECard>();
    public List<ECard> hand = new List<ECard>();
    public List<ECard> discard = new List<ECard>();

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DisplayCards()); // ~~~ shouldn't be done here
    }

    public List<ECard> DrawHand(int size) {
        for(int i = 0; i < size; ++i)
        {
            if(drawPile.Count == 0)
            {
                RefreshDrawPile();
            }
            hand.Add(drawPile[0]);
            drawPile.RemoveAt(0);
        }
        return hand;
    }

    public void DiscardHand()
    {
        discard.AddRange(hand);
        hand.Clear();
    }

    public void RefreshDrawPile()
    {
        while(discard.Count > 0)
        {
            int index = Random.Range(0, discard.Count);
            drawPile.Add(discard[index]);
            discard.RemoveAt(index);
        }
    }

    public List<ECard> NewEncounter(int size)
    {
        List<int> indices = new List<int>();
        for(int i = 0; i < deck.Count; ++i)
        {
            indices.Add(i);
        }
        while(indices.Count > 0)
        {
            int i = Random.Range(0, indices.Count);
            drawPile.Add(deck[indices[i]]);
            indices.RemoveAt(i);
        }

        return DrawHand(size);
    }

    public void EndEncounter()
    {
        hand.Clear();
        discard.Clear();
        drawPile.Clear();
    }

    void GiveStartingDeck()
    {
        deck.Clear();

        for(int i = 0; i < 5; ++i)
        {
            deck.Add(ECard.toy);
        }
    }

    IEnumerator DisplayCards()
    {
        yield return new WaitForSeconds(1.0f);
        int i = 0;
        foreach (ECard card in deck)
        {
            Instantiate(CardDictionaryRef.Instance.cards[card], new Vector3(i++ * 0.5f, 0, 0), Quaternion.AngleAxis(i * 15 + (-30), Vector3.back));

            yield return new WaitForSeconds(0.10f);
        }
    }
}
