using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static Stack<Card> Deck { get; private set; } = new Stack<Card>();
    [SerializeField] private Card _cardPrefab;
    [Header("Card Textures")]
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Sprite[] _clubTexturesInOrder = new Sprite[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Sprite[] _diamondTexturesInOrder = new Sprite[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Sprite[] _heartTexturesInOrder = new Sprite[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Sprite[] _spadeTexturesInOrder = new Sprite[13];
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
            { CardSuit.Club, _clubTexturesInOrder },
            { CardSuit.Diamond, _diamondTexturesInOrder },
            { CardSuit.Heart, _heartTexturesInOrder },
            { CardSuit.Spade, _spadeTexturesInOrder },
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
                Sprite cardTexture = cardTextures.GetValue((int)rank) as Sprite;

                spawnedCard.CreateCard(suit, rank, cardTexture);

                Debug.Log($"{spawnedCard.Value} spawned with texture {cardTexture}");
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
