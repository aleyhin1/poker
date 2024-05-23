using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

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

    private bool SetPlayers()
    {
        //bet miktarý eþitleme

        List<Player> playerList = GameManager.Instance.Players;
        playerList.Reverse();





        return false;
    }

    public void NextPlayer()
    {
        _currentPlayer = _playersQueue.Dequeue();
        _currentPlayer.IsMyTurn = false;

        if (_playersQueue.Count > 0)
            SelectPlayer();
        else
        {
            if (!SetPlayers())
            {
                PokerStateManager.Instance.NextState();
            }
        }    
    }
}