using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSequenceHandler
{
    private Queue<Player> _playersQueue;
    private Player _currentPlayer;

    public void SetPlayers(Queue<Player> playerQueue)
    {
        _playersQueue = playerQueue;
        SelectPlayer();
    }

    private void SelectPlayer()
    {
        _currentPlayer = _playersQueue.Peek();
        _currentPlayer.IsMyTurn = true;
    }

    public void NextPlayer()
    {
        _currentPlayer = _playersQueue.Dequeue();
        _currentPlayer.IsMyTurn = false;

        if (_playersQueue.Count > 0)
            SelectPlayer();
        else
        {
            PokerStateManager.Instance.NextState();
        }    
    }
}