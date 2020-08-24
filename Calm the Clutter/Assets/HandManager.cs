using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandManager : MonoBehaviour
{
    #region singleton
    private static HandManager instance;
    public static HandManager Instance
    {
        get { return instance; }
    }
    #endregion
    private void Awake()
    {
        instance = this;
    }

    DeckManager deck;
    int cardDraws = 5;
    List<ECard> handEnums = new List<ECard>();
    List<Transform> handCards = new List<Transform>();
    int cardAnimationsRunning = 0;
    List<Vector3> cardPositions = new List<Vector3>();
    int handXMin = -4;
    int handXMax = 4;
    Card activeRaycast = null;
    int oldRaycastI = -1;
    int raycastI = -1;
    // Start is called before the first frame update
    void Start()
    {
        deck = DeckManager.Instance;
        
    }

    void Update()
    {
        
        SetActiveRaycast(Raycast());
        if (Input.GetMouseButtonUp(0) && raycastI != -1 && cardAnimationsRunning == 0 && ManaGer.Instance.CardIsPlayable(activeRaycast))
        {
            activeRaycast.CardClicked();
            ManaGer.Instance.PlayCard(activeRaycast);

            activeRaycast.transform.position += Vector3.down * 5.0f;
            Destroy(activeRaycast.gameObject);

            handCards.RemoveAt(raycastI);
            deck.DiscardCard(raycastI);

            activeRaycast = null;
            oldRaycastI = -1;
            raycastI = -1;

            CalculateCardPositions();
            StartCoroutine(AnimateHandDraw());
        }
    }

    private void SetActiveRaycast(Card card)
    {
        if (activeRaycast != card)
        {
            activeRaycast?.SetAsRaycastTarget(false, oldRaycastI);
            activeRaycast = card;
            activeRaycast?.SetAsRaycastTarget(true, handCards.Count);
            if (activeRaycast == null)
            {
                raycastI = -1;
            }
            oldRaycastI = raycastI;
        }
    }

    private Card Raycast()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        if (results.Count == 0)
        {
            return null;
        }

        for (int i = handCards.Count - 1; i >= 0; --i)
        {
            foreach (RaycastResult h in results)
            {
                if (h.gameObject.layer == LayerMask.NameToLayer("CardUI"))
                {
                    Card card = h.gameObject.GetComponentInParent<Card>();
                    if (card != null && card.transform == handCards[i])
                    {
                        raycastI = i;
                        return card;
                    }
                }
            }
        }
        return null;
    }

    public void DrawNewHand()
    {
        handEnums = deck.DrawHand(cardDraws); // ~~~ should not be using new encounter every time
        foreach (ECard c in handEnums)
        {
            // Spawn card transforms below frame
            handCards.Add(Instantiate(CardDictionaryRef.Instance.cards[c], Vector3.down * 10, Quaternion.identity, gameObject.transform));
        }
        CalculateCardPositions();
        StartCoroutine(AnimateHandDraw());
    }

    private void CalculateCardPositions()
    {
        for (int i = 0; i < handCards.Count; ++i)
        {
            cardPositions.Add(new Vector3
                (handXMin + i * ((handXMax - handXMin) / ((float)handCards.Count - 1)),
                transform.position.y - 0.3f + (0.2f * Mathf.Sin(Mathf.PI * i / (handCards.Count - 1))),
                transform.position.z - i * 0.1f));
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
        cardAnimationsRunning++;

        float progress = 0.0f;
        float goalTime = 0.4f;
        Vector3 origin = handCards[cardI].position;
        Vector3 journey = cardPositions[cardI] - origin;
        handCards[cardI].GetComponentInChildren<Canvas>().sortingOrder = cardI;
        yield return null;

        while (progress <= goalTime)
        {
            progress += Time.deltaTime;

            if (progress < goalTime)
            {
                handCards[cardI].position = origin + journey * (progress / goalTime);
            }
            else
            {
                handCards[cardI].position = cardPositions[cardI];
            }
            yield return null;
        }

        yield return null;
        cardAnimationsRunning--;
    }

    public void DiscardHand()
    {
        deck.DiscardHand();
        foreach (Transform t in handCards)
        {
            t.position += Vector3.down * 5.0f;
            Destroy(t.gameObject);
        }
        handCards.Clear();
    }
}
