using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealingCards : IPokerState
{
    private const int _turnCount = 2;
    private Stack<Card> _deck;
    
    public void EnterState()
    {
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
        for (int i = 0; i < _turnCount; i++)
        {
            foreach (var player in players)
            {
                var card = _deck.Pop();

                if (player.GetType() != typeof(RealPlayer))
                    card.FaceDown();
                else 
                    card.FaceUp();
              
                player.AddCards(card);
            }
        }
    }
}