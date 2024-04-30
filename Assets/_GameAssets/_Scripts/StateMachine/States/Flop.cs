using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.FilePathAttribute;

public class Flop : IPokerState
{
    private const int CARD_COUNT = 3;
    private const float DealingCardDuration = 0.5f;

    public void EnterState()
    {
        Debug.Log("-----Flop-----");
        RevealingCards();
    }

    public void UpdateState()
    {
    
    }

    public void ExitState()
    {
    
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

    int _currentCardCount = 0;

    private void RevealingCards()
    {
        var cardLocations = GameManager.Instance.CardLocationOnTheTable;

        if (_currentCardCount < CARD_COUNT)
        {
            var card = DeckManager.Deck.Pop();
            card.FaceDown();
            DealingAnimation(card, cardLocations[_currentCardCount]);
        }
        else
        {
            SetPlayers();
        }
    }
 
    private void DealingAnimation(Card card, Transform location)
    {
        card.transform.position = new Vector3(0, 4, 0);
        card.gameObject.SetActive(true);

        GameManager.Instance.CardsOnTheTable.Add(card);

        card.transform.DOMove(location.position, DealingCardDuration)
            .SetEase(Ease.InSine)
            .OnStart(() =>
            {
                card.transform.DORotateQuaternion(location.rotation, DealingCardDuration)
                    .SetEase(Ease.InSine);
            })
            .OnComplete(() =>
            {
                card.FaceUp();
                _currentCardCount++;
                RevealingCards();
            });
    }
}