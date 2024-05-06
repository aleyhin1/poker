using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static Stack<Card> Deck { get; private set; } = new Stack<Card>();
    [SerializeField] private Card _cardPrefab;
    [Header("Card Textures")]
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Sprite[] _clubSpritesInOrder = new Sprite[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Sprite[] _diamondSpritesInOrder = new Sprite[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Sprite[] _heartSpritesInOrder = new Sprite[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Sprite[] _spadeSpritesInOrder = new Sprite[13];
    [SerializeField] private Sprite _cardBack;
    private Dictionary<CardSuit, Sprite[]> _suitTexturesPairs;

    private void Awake()
    {
        FillSuitTexturesPairs();
        FillDeck();
    }

    private void Start()
    {
        // TEST *************************************************************************************

        List<Card> hand = new List<Card>();

        Card card6 = CreateCardObject();
        card6.CreateCard(CardSuit.Spade, CardRank.Ace, null, null);
        hand.Add(card6);

        Card card7 = CreateCardObject();
        card7.CreateCard(CardSuit.Diamond, CardRank.Two, null, null);
        hand.Add(card7);

        Card card1 = CreateCardObject();
        card1.CreateCard(CardSuit.Diamond, CardRank.Four, null, null);
        hand.Add(card1);

        Card card2 = CreateCardObject();
        card2.CreateCard(CardSuit.Spade, CardRank.Queen, null, null);
        hand.Add(card2);

        Card card3 = CreateCardObject();
        card3.CreateCard(CardSuit.Diamond, CardRank.Three, null, null);
        hand.Add(card3);

        Card card4 = CreateCardObject();
        card4.CreateCard(CardSuit.Diamond, CardRank.Two, null, null);
        hand.Add(card4);

        Card card5 = CreateCardObject();
        card5.CreateCard(CardSuit.Diamond, CardRank.Five, null, null);
        hand.Add(card5);



        hand.Sort();

        Debug.Log("Sorted cards");
        foreach(Card card in hand)
        {
            Debug.Log(card.Value);
        }


        
        Debug.Log("Is Hand Flush : " + HandCalculator.IsHandFlush(hand, out List<Card> flushCardsInHand));
        Debug.Log("Flush cards in hand = ");

        if (flushCardsInHand != null)
        {
            foreach (Card card in flushCardsInHand)
            {
                Debug.Log(card.Value);
            }
        }

        Debug.Log("Is hand straight : " + HandCalculator.IsHandStraight(hand, out List<Card> straightCardsInHand));
        Debug.Log("Straight cards in hand = ");

        if (straightCardsInHand != null)
        {
            foreach (Card card in straightCardsInHand)
            {
                Debug.Log(card.Value);
            }
        }


        // TEST *************************************************************************************
    }

    private void FillSuitTexturesPairs()
    {
        _suitTexturesPairs = new Dictionary<CardSuit, Sprite[]>
        {
            { CardSuit.Club, _clubSpritesInOrder },
            { CardSuit.Diamond, _diamondSpritesInOrder },
            { CardSuit.Heart, _heartSpritesInOrder },
            { CardSuit.Spade, _spadeSpritesInOrder },
        };
    }

    private void FillDeck()
    {
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                Card spawnedCard = CreateCardObject();

                _suitTexturesPairs.TryGetValue(suit, out Sprite[] cardTextures);
                Sprite cardFront = cardTextures.GetValue((int)rank) as Sprite;

                spawnedCard.CreateCard(suit, rank, cardFront, _cardBack);
            }
        }

        ShuffleTheCards(Deck);
    }

    private Card CreateCardObject()
    {
        Card spawnedCard = Instantiate<Card>(_cardPrefab, transform);
        spawnedCard.gameObject.SetActive(false);
        Deck.Push(spawnedCard);

        return spawnedCard;
    }

    private void ShuffleTheCards(Stack<Card> deck)
    {
        Card[] array = deck.ToArray();

        var rnd = new System.Random();
        var shuffledArray = array.OrderBy(item => rnd.Next()).ToArray();

        deck.Clear(); 

        foreach (var card in shuffledArray)
        {
            deck.Push(card);
        }
    }
}
