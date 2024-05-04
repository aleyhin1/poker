using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DealingCards : IPokerState
{
    private const int CARD_COUNT = 2;
    private int _currentCardCount = 0;
    private int _currentPlayerIndex = 0;
    private List<Player> _players;

    private const float dealingCardDuration = 0.5f;

    public void EnterState()
    {
        Debug.Log("-DealingCards-");


        GameManager.Instance.DealerController.CollectBets();

        List<Player> players = GameManager.Instance.Players;
     
        DealingCard(players);
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
        if (_currentCardCount < CARD_COUNT && _currentPlayerIndex < _players.Count)
        {
            var card = DeckManager.Deck.Pop();

            if (_players[_currentPlayerIndex].GetType() != typeof(RealPlayer))
                card.FaceDown();
            else
                card.FaceUp();

            DealingAnimation(card, _players[_currentPlayerIndex]);
        }
        else
        {
            PokerStateManager.Instance.EnterState(PokerState.Preflop);
        }
    }

    private void DealingAnimation(Card card , Player player)
    {
        card.transform.position = new Vector3(0, 4, 0);
        var cardTransformData = player.AddCards(card);

        card.transform.DOMove(cardTransformData.pos, dealingCardDuration)
            .SetEase(Ease.InSine)
            .OnStart(() =>
            {
                card.transform.DORotateQuaternion(cardTransformData.rot, dealingCardDuration)
                    .SetEase(Ease.InSine);
            })
            .OnComplete(() =>
            {
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