using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class DealerController : MonoSingleton<DealerController>
{
    private const float DEALING_DURATION= 0.5f;

    private int _startingIndex;
    private int _currentCardCount;
    private int _cardCount;
    private List<Transform> _cardLocationOnTheTable;

    public void StartDealing(int cardCount, int cardLocationIndex)
    {
        _cardLocationOnTheTable = GameManager.Instance.CardLocationOnTheTable;

        _currentCardCount = 0;
        _cardCount = cardCount;
        _startingIndex = cardLocationIndex;

        RevealingCards();
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

        card.transform.DOMove(location.position, DEALING_DURATION)
            .SetEase(Ease.InSine)
            .OnStart(() =>
            {
                card.transform.DORotateQuaternion(location.rotation, DEALING_DURATION)
                    .SetEase(Ease.InSine);
            })
            .OnComplete(() =>
            {
                card.FaceUp();
                _startingIndex++;
                _currentCardCount++;
                RevealingCards();
            });
    }
}