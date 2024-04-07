using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static Stack<Card> Deck { get; private set; } = new Stack<Card>();
    [SerializeField] private Card _cardPrefab;

    private void Awake()
    {
        FillDeck();
    }

    private void FillDeck()
    {
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                Card spawnedCard = CreateCardObject();
                spawnedCard.CreateCard(suit, rank);

                Debug.Log($"{spawnedCard.CardValue} spawned");
            }
        }
    }

    private Card CreateCardObject()
    {
        Card spawnedCard = Instantiate<Card>(_cardPrefab, transform);
        spawnedCard.gameObject.SetActive(false);
        Deck.Push(spawnedCard);

        return spawnedCard;
    }
}
