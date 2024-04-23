 using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class DealingCards : IPokerState
{
    private const int _cardCount = 2;
    private Stack<Card> _deck;

    private int _currentCardCount = 0;
    private int _currentPlayerIndex = 0;
    private List<Player> _players;

    public void EnterState()
    {
        Debug.Log("-DealingCards-");

        List<Player> players = GameManager.Instance.GetPlayers;
        _deck = DeckManager.Deck;

        DealingCard(players);
    }

    public void UpdateState()
    {
         
    }

    public void ExitState()
    {

    }

    private void DealingCard(List<Player> players)
    {
        _players = players;
        _currentCardCount = 0;
        _currentPlayerIndex = 0;
        Dealing();
    }

    private void Dealing()
    {
        if (_currentCardCount < _cardCount)
        {
            if (_currentPlayerIndex > _players.Count)
                return;

            var card = _deck.Pop();

            if (_players[_currentPlayerIndex].GetType() != typeof(RealPlayer))
                card.FaceDown();
            else
                card.FaceUp();

            DealingAnimation(card, _players[_currentPlayerIndex]);
        }
        else
        {
            GameManager.Instance.GetPokerStateManager.EnterPreflopState();
        }
    }

    private void DealingAnimation(Card card , Player player)
    {
        card.transform.position = new Vector3(0, 4, 0);
        var cardTransformData = player.AddCards(card);

        card.transform.DOMove(cardTransformData.pos, 1f)
        .SetEase(Ease.InSine)
        .OnComplete(() =>
        {
            card.transform.DORotateQuaternion(cardTransformData.rot, 0.7f)
            .SetEase(Ease.InSine);

            _currentPlayerIndex++;
            if (_currentPlayerIndex == _players.Count)
            {
                _currentCardCount++;
                _currentPlayerIndex = 0;
            }
            Dealing();
        });
    }
}