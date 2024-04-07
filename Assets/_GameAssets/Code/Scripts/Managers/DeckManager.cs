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
    [SerializeField] private Texture2D[] _clubTexturesInOrder = new Texture2D[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Texture2D[] _diamondTexturesInOrder = new Texture2D[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Texture2D[] _heartTexturesInOrder = new Texture2D[13];
    [Tooltip("Order in according to CardRank Enum")]
    [SerializeField] private Texture2D[] _spadeTexturesInOrder = new Texture2D[13];
    private Dictionary<CardSuit, Texture2D[]> _suitTexturesPairs;


    private void Awake()
    {
        FillSuitTexturesPairs();
        FillDeck();
    }

    private void FillSuitTexturesPairs()
    {
        _suitTexturesPairs = new Dictionary<CardSuit, Texture2D[]>
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

                _suitTexturesPairs.TryGetValue(suit, out Texture2D[] cardTextures);
                Texture2D cardTexture = cardTextures.GetValue((int)rank) as Texture2D;

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
