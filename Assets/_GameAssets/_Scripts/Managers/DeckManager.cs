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