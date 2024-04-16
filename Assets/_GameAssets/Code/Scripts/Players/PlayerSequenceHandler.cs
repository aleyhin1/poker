using System;
using System.Collections.Generic;

public class PlayerSequenceHandler
{
    public Action NextPlayerAction;

    private Queue<Player> _playersQueue;
    private Player _currentPlayer;

    public PlayerSequenceHandler(Queue<Player> playerQueue)
    {
        _playersQueue = playerQueue;
        NextPlayerAction += NextPlayer;
    }

    public void SelectPlayer()
    {
        _currentPlayer = _playersQueue.Peek();
        _currentPlayer.IsMyTurn = true;
    }

    public void OnDisable()
    {
        NextPlayerAction -= NextPlayer; 
    }

    private void NextPlayer()
    {
        _currentPlayer = _playersQueue.Dequeue();
        _currentPlayer.IsMyTurn = false;

        if (_playersQueue.Count > 0)
            SelectPlayer();
        else
        {
            GameManager.Instance.NextState();
        }
    }
}