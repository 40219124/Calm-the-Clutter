using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaGer : MonoBehaviour
{
    #region singleton
    private static ManaGer instance;
    public static ManaGer Instance
    {
        get { return instance; }
    }
    #endregion
    private void Awake()
    {
        instance = this;
    }


    [SerializeField]
    Text manaText;

    [SerializeField]
    int manaPerTurn = 3;
    int currentMana = 0;
    public int CurrentMana
    {
        get { return currentMana; }
        set
        {
            currentMana = value;
            manaText.text = currentMana.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        manaText.text = currentMana.ToString();
    }

    public bool CardIsPlayable(Card card)
    {
        return card.basicCardInfo.cardMana <= CurrentMana;
    }

    public bool PlayCard(Card card)
    {
        bool returnBool = CardIsPlayable(card);

        if (returnBool)
        {
            CurrentMana -= card.basicCardInfo.cardMana;
        }

        return returnBool;
    }

    public void StartTurn()
    {
        CurrentMana += manaPerTurn;
    }

    public void EndTurn()
    {
        CurrentMana = 0;
    }
}
