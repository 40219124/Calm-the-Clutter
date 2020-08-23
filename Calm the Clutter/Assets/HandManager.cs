using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    DeckManager deck;
    int cardDraws = 5;
    List<ECard> handEnums = new List<ECard>();
    List<Transform> handCards = new List<Transform>();
    List<Vector3> cardPositions = new List<Vector3>();
    int handXMin = -4;
    int handXMax = 4;
    // Start is called before the first frame update
    void Start()
    {
        deck = DeckManager.Instance;
        DrawNewHand();
        StartCoroutine(AnimateHandDraw());
    }

    public void DrawNewHand()
    {
        handEnums = deck.NewEncounter(cardDraws); // ~~~ should not be using new encounter every time
        foreach(ECard c in handEnums)
        {
            // Spawn card transforms below frame
            handCards.Add(Instantiate(CardDictionaryRef.Instance.cards[c], Vector3.down * 10, Quaternion.identity, gameObject.transform));
        }
        CalculateCardPositions();
    }

    private void CalculateCardPositions()
    {
        for(int i = 0; i < handCards.Count; ++i)
        {
            cardPositions.Add(new Vector3
                (handXMin + i * ((handXMax - handXMin) / (float)handCards.Count),
                transform.position.y + 1.0f + (0.2f * Mathf.Sin(Mathf.PI * i/(handCards.Count - 1))), 
                transform.position.z));
        }
    }

    private IEnumerator AnimateHandDraw()
    {
        for (int i = 0; i < handCards.Count; ++i)
        {
            StartCoroutine(AnimateCardDraw(i));
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    private IEnumerator AnimateCardDraw(int cardI)
    {
        float progress = 0.0f;
        Vector3 origin = handCards[cardI].position;
        Vector3 journey = cardPositions[cardI] - origin;
        yield return null;

        while (progress <= 1.0f)
        {
            progress += Time.deltaTime;

            if (progress < 1)
            {
                handCards[cardI].position = origin + journey * progress;
            }
            else
            {
                handCards[cardI].position = cardPositions[cardI];
            }
            yield return null;
        }

        yield return null;
    }
}
