using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DeckManager : MonoBehaviour
{
    public static Stack<Card> Deck { get; private set; } = new Stack<Card>();
    private static List<Card> Cards = new List<Card>();

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

    public static void ResetDeck()
    {
        foreach (var card in Cards)
        {
            card.FaceDown();
            card.transform.position = Vector3.zero;
            card.transform.rotation = Quaternion.Euler(0,0,0);
            card.gameObject.SetActive(false);
        }

        ShuffleTheCards(Cards);
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

        ShuffleTheCards(Cards);
    }

    private Card CreateCardObject()
    {
        Card spawnedCard = Instantiate<Card>(_cardPrefab, transform);
        spawnedCard.gameObject.SetActive(false);
        spawnedCard.GetComponent<SpriteRenderer>().sortingOrder = 3;
        Cards.Add(spawnedCard);

        return spawnedCard;
    }

    private static void ShuffleTheCards(List<Card> cards)
    {
        Card[] array = cards.ToArray();

        var rnd = new System.Random();
        var shuffledArray = array.OrderBy(item => rnd.Next()).ToArray();

        Deck.Clear(); 

        foreach (var card in shuffledArray)
        {
            Deck.Push(card);
        }
    }
}
