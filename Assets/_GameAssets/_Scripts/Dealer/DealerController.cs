using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class DealerController : MonoSingleton<DealerController>
{
    private const float DURATION = 0.5f;

    [SerializeField] private GameObject _betBox;
    [SerializeField] private TextMeshPro _betText;

    [SerializeField] private int _totalBet = 0;

    private int _startingIndex;
    private int _currentCardCount;
    private int _cardCount;
    private List<Transform> _cardLocationOnTheTable;

    public bool BetsPlaced { get; set; }
    public int HighestBet {  get; set; }

    private void Start()
    {
        _betBox.SetActive(false);
    }

    public void StartDealing(int cardCount, int cardLocationIndex)
    {
        CollectBets();
        _cardLocationOnTheTable = GameManager.Instance.CardLocationOnTheTable;
        _currentCardCount = 0;
        _cardCount = cardCount;
        _startingIndex = cardLocationIndex;

        RevealingCards();
    }

    public void CollectBets()
    {
        List<Player> players = GameManager.Instance.Players;
        int betAmount = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].IsBet)
            {
                betAmount += players[i].LastBet;
                CollectAnimation(players[i]);
            }
        }
        _totalBet += betAmount;

        _betText.text = "$" + _totalBet.ToString();
        _betBox.SetActive(true);
    }

    private void CollectAnimation(Player player)
    {
        player.BetBox.transform.DOMove(_betBox.transform.position, DURATION)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            player.ResetBetBox();
        });
    }

    private void RevealingCards()
    {
        if (_currentCardCount < _cardCount)
        {
            var card = DeckManager.Deck.Pop();
            card.FaceDown();
            DealingAnimation(card, _cardLocationOnTheTable[_startingIndex]);
        }
        else
        {
            BetsPlaced = false;
            SetPlayers();
        }
    }

    private void SetPlayers()
    {
        List<Player> players = GameManager.Instance.Players;
        Queue<Player> playerQueue = new Queue<Player>();

        foreach (Player player in players)
        {
            if (!player.IsFold)
            {
                playerQueue.Enqueue(player);
            }
        }
        GameManager.Instance.PlayerSequenceHandler.SetPlayers(playerQueue);
    }

    private void DealingAnimation(Card card, Transform location)
    {
        card.transform.position = new Vector3(0, 4, 0);
        card.gameObject.SetActive(true);

        GameManager.Instance.CardsOnTheTable.Add(card);

        card.transform.DOMove(location.position, DURATION)
            .SetEase(Ease.InSine)
            .OnStart(() =>
            {
                card.GetComponent<SpriteRenderer>().sortingOrder = 10;
                card.transform.DORotateQuaternion(location.rotation, DURATION)
                    .SetEase(Ease.InSine);
            })
            .OnComplete(() =>
            {
                card.GetComponent<SpriteRenderer>().sortingOrder = 1;
                card.FaceUp();
                _startingIndex++;
                _currentCardCount++;
                RevealingCards();
            });
    }

    public void CheckPlayers()
    {
        List<Player> playerList = GameManager.Instance.Players;
     
        foreach (var player in playerList)
        {
            if (!player.IsFold && player.IsRaise && player.LastBet > HighestBet)
                HighestBet = player.LastBet;
        }

        Queue<Player> lastPlayers = new Queue<Player>();

        foreach (var player in playerList)
        {
            if (!player.IsFold && player.LastBet < HighestBet)
            {
                player.CallingTheBet = true;
                lastPlayers.Enqueue(player);
            }
        }

        if (lastPlayers.Count > 0)
            GameManager.Instance.PlayerSequenceHandler.SetPlayers(lastPlayers);
        else
            PokerStateManager.Instance.NextState();
    }

}